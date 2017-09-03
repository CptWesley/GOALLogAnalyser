using System;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using System.Xml;
using System.Xml.Linq;
using GOALLogAnalyser.Analyzation.Cycles;
using GOALLogAnalyser.Analyzation.Modules;
using GOALLogAnalyser.Analyzation.Queries;
using GOALLogAnalyser.Exceptions;

namespace GOALLogAnalyser.Analyzation.Agents
{
    /// <summary>
    /// Class that represents a performance profile of an agent.
    /// </summary>
    public class AgentProfile
    {
        
        private static Regex _queryRegex = new Regex(String.Join("|", new[] {
            @"\[.+\]",
            @"'.+'",
            @"(?<=\W)\d+",
            @"true|false",
            @"\w+(?=\)|,)"
        }));

        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>
        /// The type.
        /// </value>
        public string Type { get; }
        /// <summary>
        /// Gets the module profiles.
        /// </summary>
        /// <value>
        /// The module profiles.
        /// </value>
        public ModuleProfileCollection ModuleProfiles { get; }
        /// <summary>
        /// Gets the cycle profile.
        /// </summary>
        /// <value>
        /// The cycle profile.
        /// </value>
        public CycleProfile CycleProfile { get; }
        /// <summary>
        /// Gets the query profiles.
        /// </summary>
        /// <value>
        /// The query profiles.
        /// </value>
        public QueryProfileCollection QueryProfiles { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentProfile"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public AgentProfile(string name, string type)
        {
            Name = name;
            Type = type;
            ModuleProfiles = new ModuleProfileCollection();
            CycleProfile = new CycleProfile();
            QueryProfiles = new QueryProfileCollection();
        }

        /// <summary>
        /// Creates an agent profile with the specified name based on the given logs.
        /// </summary>
        /// <param name="logFileName">The log file path.</param>
        /// <returns>A new agentprofile based on the supplied logs.</returns>
        public static AgentProfile Create(string logFileName)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(logFileName);

            Regex rgx = new Regex(@"(.+)_(.+)_(.+)\.txt");
            Match match = rgx.Match(fileNameWithoutExtension);
            if (!match.Success)
            {
                throw new InvalidFileNameException();
            }

            string agentName = match.Groups[1].Value;
            string agentType;
            rgx = new Regex(@"(.+)(_\d+)");
            match = rgx.Match(agentName);
            if (match.Success)
            {
                agentType = match.Groups[1].Value;
            }
            else
            {
                agentType = agentName;
            }

            AgentProfile result = new AgentProfile(agentName, agentType);

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            using (XmlReader reader = XmlReader.Create(logFileName, settings))
            {
                try
                {
                    Stack<long> enterTimes = new Stack<long>();
                    long lastCycleTime = -1;
                    long lastModuleTime = 0;
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name == "record")
                        {
                            XElement el = (XElement)XNode.ReadFrom(reader);
                            string message = null;
                            long msgTime = -1;
                            foreach (XElement child in el.Descendants())
                            {
                                switch (child.Name.ToString())
                                {
                                    case "message":
                                        message = child.Value;
                                        break;
                                    case "millis":
                                        msgTime = long.Parse(child.Value);
                                        break;
                                }
                            }

                            if (message == null || msgTime == -1)
                                continue;

                            Tuple<uint, string[]> msg = RecordMessageParser.Parse(message);

                            switch (msg.Item1)
                            {
                                case RecordMessageType.ModuleEntryType:
                                    enterTimes.Push(msgTime);
                                    break;
                                case RecordMessageType.ModuleExitType:
                                    long executionTime = msgTime - enterTimes.Pop();
                                    int index = result.ModuleProfiles.IndexOf(msg.Item2[0]);
                                    if (index == -1)
                                    {
                                        ModuleProfile mp = new ModuleProfile(msg.Item2[0]);

                                        mp.AddExecution(executionTime, lastModuleTime);
                                        result.ModuleProfiles.Add(mp);
                                    }
                                    else
                                    {
                                        result.ModuleProfiles[index].AddExecution(executionTime, lastModuleTime);
                                    }
                                    lastModuleTime = executionTime;
                                    break;
                                case RecordMessageType.CycleStatisticsType:
                                    long time = msgTime - lastCycleTime;
                                    if (lastCycleTime == -1)
                                        time = 0;
                                    Cycle cycle = new Cycle(time);

                                    if (!string.IsNullOrEmpty(msg.Item2[0]))
                                        cycle.Actions = int.Parse(msg.Item2[0]);
                                    if (!string.IsNullOrEmpty(msg.Item2[1]))
                                        cycle.MessagesSent = int.Parse(msg.Item2[1]);
                                    if (!string.IsNullOrEmpty(msg.Item2[2]))
                                        cycle.Queries = int.Parse(msg.Item2[2]);
                                    if (!string.IsNullOrEmpty(msg.Item2[3]))
                                        cycle.Beliefs = int.Parse(msg.Item2[3]);
                                    if (!string.IsNullOrEmpty(msg.Item2[4]))
                                        cycle.Goals = int.Parse(msg.Item2[4]);
                                    if (!string.IsNullOrEmpty(msg.Item2[5]))
                                        cycle.MessagesReceived = int.Parse(msg.Item2[5]);
                                    if (!string.IsNullOrEmpty(msg.Item2[6]))
                                        cycle.Percepts = int.Parse(msg.Item2[6]);

                                    result.CycleProfile.Add(cycle);
                                    lastCycleTime = msgTime;
                                    break;
                                case RecordMessageType.QuerySuccessType:
                                    AddQuery(result, msg.Item2[0], true);
                                    break;
                                case RecordMessageType.QueryFailureType:
                                    AddQuery(result, msg.Item2[0], false);
                                    break;
                            }


                        }
                    }
                }
                catch
                {
                    throw new InvalidFileContentException();
                }
            }
            return result;
        }

        private static void AddQuery(AgentProfile ap, string message, bool hit)
        {
            int count = 0;
            string query = _queryRegex.Replace(message, m => "VAR" + count++);

            int index = ap.QueryProfiles.IndexOf(query);
            if (index == -1)
            {
                QueryProfile qp = new QueryProfile(query);

                qp.Add(hit);
                ap.QueryProfiles.Add(qp);
            }
            else
            {
                ap.QueryProfiles[index].Add(hit);
            }
        }
    }
}

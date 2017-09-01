using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using GOALLogAnalyser.Analyzation.Cycles;
using GOALLogAnalyser.Analyzation.Modules;
using GOALLogAnalyser.Analyzation.Queries;
using GOALLogAnalyser.Analyzation.Threads;
using GOALLogAnalyser.Parsing;

namespace GOALLogAnalyser.Analyzation.Agents
{
    /// <summary>
    /// Class that represents a performance profile of an agent.
    /// </summary>
    public class AgentProfile
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; }
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
        public AgentProfile(string name)
        {
            Name = name;
            ModuleProfiles = new ModuleProfileCollection();
            CycleProfile = new CycleProfile();
            QueryProfiles = new QueryProfileCollection();
        }

        /// <summary>
        /// Creates an agent profile with the specified name based on the given records.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="records">The records.</param>
        /// <returns>A new agentprofile based on the supplied records.</returns>
        public static AgentProfile Create(string name, Record[] records, RecordFinder recordFinder)
        {
            AgentProfile result = new AgentProfile(name);

            Stack<long> enterTimes = new Stack<long>();
            long lastCycleTime = -1;
            foreach (Record r in records)
            {
                Tuple<uint, string[]> msg = RecordMessageParser.Parse(r.Message);

                switch (msg.Item1)
                {
                    case RecordMessageType.ModuleEntryType:
                        enterTimes.Push(r.Time);
                        break;
                    case RecordMessageType.ModuleExitType:
                        long executionTime = r.Time - enterTimes.Pop();
                        int index = result.ModuleProfiles.IndexOf(msg.Item2[0]);
                        if (index == -1)
                        {
                            ModuleProfile mp = new ModuleProfile(msg.Item2[0]);

                            mp.AddExecution(executionTime);
                            result.ModuleProfiles.Add(mp);
                        }
                        else
                        {
                            result.ModuleProfiles[index].AddExecution(executionTime);
                        }
                        break;
                    case RecordMessageType.CycleStatisticsType:
                        long time = r.Time - lastCycleTime;
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
                        lastCycleTime = r.Time;
                        break;
                    case RecordMessageType.QuerySuccessType:
                        AddQuery(result, msg.Item2[0], true);
                        break;
                    case RecordMessageType.QueryFailureType:
                        AddQuery(result, msg.Item2[0], false);
                        break;
                }
            }

            return result;
        }

        private static void AddQuery(AgentProfile ap, string message, bool hit)
        {
            //string pattern = @"((?<=\W)\d+)|('.+')";
            int count = 0;
            //string query = Regex.Replace(message, pattern, m => "Var" + count++);

            string[] patterns =
            {
                @"\[.+\]",
                @"'.+'",
                @"(?<=\W)\d+",
                @"true|false",
                @"\w+(?=\)|,)"
            };

            string query = message;
            foreach (string pattern in patterns)
                query = Regex.Replace(query, pattern, m => "VAR" + count++);

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

        public void AddProfile(string fileName)
        {
            
        }

        private static long FindTimeSincePreviousRecord(Record r, RecordFinder recordFinder)
        {
            int index = recordFinder.IndexOf(r.Thread, r.Sequence);

            if (index == 0)
                return 0;

            return r.Time - recordFinder.Threads[r.Thread][index - 1].Time;
        }
    }
}

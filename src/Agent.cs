using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using GOALLogAnalyser.Exceptions;
using GOALLogAnalyser.Parsing;

namespace GOALLogAnalyser
{
    /// <summary>
    /// Class that holds agent info.
    /// </summary>
    public class Agent
    {
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

        private List<Record> _records;

        /// <summary>
        /// Initializes a new instance of the <see cref="Agent"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <param name="type">The type.</param>
        public Agent(string name, string type)
        {
            Name = name;
            Type = type;
            _records = new List<Record>();
        }

        /// <summary>
        /// Gets the records.
        /// </summary>
        /// <returns></returns>
        public Record[] GetRecords()
        {
            return _records.ToArray();
        }

        /// <summary>
        /// Adds a record.
        /// </summary>
        /// <param name="record">The record.</param>
        public void AddRecord(Record record)
        {
            _records.Add(record);
        }

        /// <summary>
        /// Removes a record.
        /// </summary>
        /// <param name="record">The record.</param>
        public void RemoveRecord(Record record)
        {
            _records.Remove(record);
        }

        /// <summary>
        /// Clears the records.
        /// </summary>
        public void ClearRecords()
        {
            _records = new List<Record>();
        }

        /// <summary>
        /// Creates an Agent from the specified file name.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        /// <returns>An agent.</returns>
        /// <exception cref="InvalidFileNameException"></exception>
        public static Agent Create(string fileName)
        {
            string fileNameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

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

            Agent agent = new Agent(agentName, agentType);

            LogParser parser = new LogParser(fileName);
            foreach (Record r in parser.Parse())
            {
                agent.AddRecord(r);
            }

            return agent;
        }

        /// <summary>
        /// Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return "Agent(Name: " + Name + ", Type: " + Type + ")";
        }
    }
}

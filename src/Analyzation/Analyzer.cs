using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using GOALLogAnalyser.Analyzation.Agents;
using GOALLogAnalyser.Analyzation.Threads;
using GOALLogAnalyser.Parsing;

namespace GOALLogAnalyser.Analyzation
{
    /// <summary>
    /// Class that holds an object that is able to analyze records.
    /// </summary>
    public class Analyzer
    {
        /// <summary>
        /// Gets the profiles.
        /// </summary>
        /// <value>
        /// The profiles.
        /// </value>
        public List<AgentTypeProfile> Profiles { get; private set; }
        /// <summary>
        /// Gets a value indicating whether this <see cref="Analyzer"/> is done.
        /// </summary>
        /// <value>
        ///   <c>true</c> if done; otherwise, <c>false</c>.
        /// </value>
        public bool Done { get; private set; }
        /// <summary>
        /// Gets the current amount of profiles that have been created.
        /// </summary>
        /// <value>
        /// The progress.
        /// </value>
        public int Progress { get; private set; }
        /// <summary>
        /// Gets the total amount of profiles to be created.
        /// </summary>
        /// <value>
        /// The total.
        /// </value>
        public int Total { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Analyzer"/> class.
        /// </summary>
        public Analyzer()
        {
            Done = false;
            Profiles = null;
            Progress = 0;
            Total = 0;
        }

        /// <summary>
        /// Analyzes the specified agents.
        /// </summary>
        /// <param name="agents">The agents.</param>
        public void Analyze(Agent[] agents)
        {
            Done = false;
            Progress = 0;
            Total = agents.Length;

            Dictionary<int, List<Record>> threads = GenerateThreadDictionary(agents);
            Dictionary<string, List<Agent>> sortedAgents = SortAgents(agents);
            Profiles = GenerateProfiles(sortedAgents, threads);

            Done = true;
        }

        /// <summary>
        /// Sorts the agents on agent types.
        /// </summary>
        /// <param name="agents">The agents.</param>
        /// <returns>A <see cref="Dictionary{AgentType,List}"/> containing a <see cref="List{Agent}"/> per agent type.</returns>
        private Dictionary<string, List<Agent>> SortAgents(Agent[] agents)
        {
            Dictionary<string, List<Agent>> result = new Dictionary<string, List<Agent>>();

            foreach (Agent a in agents)
            {
                if (result.ContainsKey(a.Type))
                {
                    result[a.Type].Add(a);
                }
                else
                {
                    result.Add(a.Type, new List<Agent>{a});
                }
            }

            return result;
        }

        /// <summary>
        /// Generates the profiles of the specified agents.
        /// </summary>
        /// <param name="agents">The agents.</param>
        /// <returns>A List of generated profiles.</returns>
        private List<AgentTypeProfile> GenerateProfiles(Dictionary<string, List<Agent>> agents, Dictionary<int, List<Record>> threads)
        {
            List<AgentTypeProfile> result = new List<AgentTypeProfile>();

            List<Task> tasks = new List<Task>();
            foreach (KeyValuePair<string, List<Agent>> kv in agents)
            {
                AgentTypeProfile profile = new AgentTypeProfile(kv.Key);
                foreach (Agent a in kv.Value)
                {
                    tasks.Add(Task.Factory.StartNew(() =>
                    {
                        profile.Add(AgentProfile.Create(a.Name, a.GetRecords()));
                        ++Progress;
                    }));
                }

                result.Add(profile);
            }
            Task.WaitAll(tasks.ToArray());

            return result;
        }

        /// <summary>
        /// Generates the thread dictionary.
        /// </summary>
        /// <param name="agents">The agents.</param>
        /// <returns></returns>
        private Dictionary<int, List<Record>> GenerateThreadDictionary(Agent[] agents)
        {
            Dictionary<int, List<Record>> result = new Dictionary<int, List<Record>>();

            foreach (Agent a in agents)
            {
                foreach (Record r in a.GetRecords())
                {
                    if (!result.ContainsKey(r.Thread))
                        result.Add(r.Thread, new List<Record>(1));
                    result[r.Thread].Add(r);
                }
            }

            foreach (KeyValuePair<int, List<Record>> kv in result)
                new RecordSorter(kv.Value).Sort();


            return result;
        }
    }
}

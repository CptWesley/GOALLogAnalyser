using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using GOALLogAnalyser.Analyzation.Agents;
using GOALLogAnalyser.Analyzation.Cycles;
using GOALLogAnalyser.Analyzation.Modules;

namespace GOALLogAnalyser.Output
{
    /// <summary>
    /// Class that holds method for dumping the gathered info into text files.
    /// </summary>
    public class TextGenerator
    {
        /// <summary>
        /// Gets the target directory.
        /// </summary>
        /// <value>
        /// The target directory.
        /// </value>
        public string Directory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TextGenerator"/> class.
        /// </summary>
        /// <param name="directory">The target directory.</param>
        public TextGenerator(string directory)
        {
            Directory = directory;
        }

        /// <summary>
        /// Writes the specified agents to files.
        /// </summary>
        /// <param name="agents">The agents.</param>
        public void Write(List<AgentTypeProfile> agents)
        {
            string agentsDir = Path.Combine(Directory, "agents");
            string globalDir = Path.Combine(Directory, "global");
            System.IO.Directory.CreateDirectory(Directory);
            System.IO.Directory.CreateDirectory(agentsDir);
            System.IO.Directory.CreateDirectory(globalDir);

            foreach (AgentTypeProfile tp in agents)
            {
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("Type: " + tp.Name);
                sb.AppendLine("Count: " + tp.AgentProfiles.Count);

                foreach (ModuleProfile mp in tp.ModuleProfiles.OrderByDescending(o => o.TotalExecutionTime))
                {
                    AppendModuleProfile(sb, mp);
                }
                AppendCycleProfile(sb, tp.CycleProfile);
                File.WriteAllText(Path.Combine(globalDir, tp.Name + ".txt"), sb.ToString());

                foreach (AgentProfile ap in tp.AgentProfiles)
                {
                    sb.Clear();
                    foreach (ModuleProfile mp in ap.ModuleProfiles.OrderByDescending(o => o.TotalExecutionTime))
                    {
                        AppendModuleProfile(sb, mp);
                    }
                    AppendCycleProfile(sb, ap.CycleProfile);
                    File.WriteAllText(Path.Combine(agentsDir, ap.Name + ".txt"), sb.ToString());
                }
            }
        }

        /// <summary>
        /// Appends the <see cref="ModuleProfile"/> to the <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/>.</param>
        /// <param name="mp">The <see cref="ModuleProfile"/>.</param>
        private static void AppendModuleProfile(StringBuilder sb, ModuleProfile mp)
        {
            sb.AppendLine();
            sb.AppendLine("Module: " + mp.Name);
            sb.AppendLine("Executions: " + mp.Executions);
            sb.AppendLine("Average Execution Time: " + mp.AverageExecutionTime);
            sb.AppendLine("Total Execution Time: " + mp.TotalExecutionTime);
            sb.AppendLine("Shortest Execution Time: " + mp.ShortestExecutionTime);
            sb.AppendLine("Longest Execution Time: " + mp.LongestExecutionTime);
        }

        /// <summary>
        /// Appends the <see cref="CycleProfile"/> to the <see cref="StringBuilder"/>.
        /// </summary>
        /// <param name="sb">The <see cref="StringBuilder"/>.</param>
        /// <param name="cp">The <see cref="CycleProfile"/>.</param>
        private static void AppendCycleProfile(StringBuilder sb, CycleProfile cp)
        {
            sb.AppendLine();
            sb.AppendLine("Cycles: " + cp.Count);
            // Time
            sb.AppendLine("Total Time: " + cp.TotalTime);
            sb.AppendLine("Average Time: " + cp.AverageTime);
            sb.AppendLine("Lowest Time: " + cp.LowestTime);
            sb.AppendLine("Highest Time: " + cp.HighestTime);
            // Actions
            sb.AppendLine("Total Actions: " + cp.TotalActions);
            sb.AppendLine("Average Actions: " + cp.AverageActions);
            sb.AppendLine("Lowest Actions: " + cp.LowestActions);
            sb.AppendLine("Highest Actions: " + cp.HighestActions);
            // Queries
            sb.AppendLine("Total Queries: " + cp.TotalQueries);
            sb.AppendLine("Average Queries: " + cp.AverageQueries);
            sb.AppendLine("Lowest Queries: " + cp.LowestQueries);
            sb.AppendLine("Highest Queries: " + cp.HighestQueries);
            // Beliefs
            sb.AppendLine("Total Beliefs: " + cp.TotalBeliefs);
            sb.AppendLine("Average Beliefs: " + cp.AverageBeliefs);
            sb.AppendLine("Lowest Beliefs: " + cp.LowestBeliefs);
            sb.AppendLine("Highest Beliefs: " + cp.HighestBeliefs);
            // Percepts
            sb.AppendLine("Total Percepts: " + cp.TotalPercepts);
            sb.AppendLine("Average Percepts: " + cp.AveragePercepts);
            sb.AppendLine("Lowest Percepts: " + cp.LowestPercepts);
            sb.AppendLine("Highest Percepts: " + cp.HighestPercepts);
            // Goals
            sb.AppendLine("Total Goals: " + cp.TotalGoals);
            sb.AppendLine("Average Goals: " + cp.AverageGoals);
            sb.AppendLine("Lowest Goals: " + cp.LowestGoals);
            sb.AppendLine("Highest Goals: " + cp.HighestGoals);
            // Messages Sent
            sb.AppendLine("Total Messages Sent: " + cp.TotalMessagesSent);
            sb.AppendLine("Average Messages Sent: " + cp.AverageMessagesSent);
            sb.AppendLine("Lowest Messages Sent: " + cp.LowestMessagesSent);
            sb.AppendLine("Highest Messages Sent: " + cp.HighestMessagesSent);
            // Messages Received
            sb.AppendLine("Total Messages Received: " + cp.TotalMessagesReceived);
            sb.AppendLine("Average Messages Received: " + cp.AverageMessagesReceived);
            sb.AppendLine("Lowest Messages Received: " + cp.LowestMessagesReceived);
            sb.AppendLine("Highest Messages Received: " + cp.HighestMessagesReceived);
        }
    }
}

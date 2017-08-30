using System.Collections.Generic;
using GOALLogAnalyser.Analyzation.Cycles;
using GOALLogAnalyser.Analyzation.Modules;
using GOALLogAnalyser.Analyzation.Queries;

namespace GOALLogAnalyser.Analyzation.Agents
{
    /// <summary>
    /// Class that contains performance data of an entire agent type.
    /// </summary>
    public class AgentTypeProfile
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
        /// Gets the agent profiles.
        /// </summary>
        /// <value>
        /// The agent profiles.
        /// </value>
        public List<AgentProfile> AgentProfiles { get; }
        /// <summary>
        /// Gets the cycle profile.
        /// </summary>
        /// <value>
        /// The cycle profile.
        /// </value>
        public CycleProfile CycleProfile { get; private set; }
        /// <summary>
        /// Gets the query profiles.
        /// </summary>
        /// <value>
        /// The query profiles.
        /// </value>
        public QueryProfileCollection QueryProfiles { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="AgentTypeProfile"/> class.
        /// </summary>
        /// <param name="name">The name.</param>
        public AgentTypeProfile(string name)
        {
            Name = name;
            ModuleProfiles = new ModuleProfileCollection();
            AgentProfiles = new List<AgentProfile>();
            CycleProfile = new CycleProfile();
            QueryProfiles = new QueryProfileCollection();
        }

        /// <summary>
        /// Adds the specified agent profile.
        /// </summary>
        /// <param name="ap">The agent profile.</param>
        public void Add(AgentProfile ap)
        {
            AgentProfiles.Add(ap);

            // Add all module profiles.
            foreach (ModuleProfile mp in ap.ModuleProfiles)
            {
                bool contained = false;

                for (int i = 0; i < ModuleProfiles.Count; ++i)
                {
                    if (mp.Name == ModuleProfiles[i].Name)
                    {
                        contained = true;
                        ModuleProfiles[i] += mp;
                        break;
                    }
                }

                if (!contained)
                {
                    ModuleProfiles.Add(mp);
                }
            }

            // Add all query profiles.
            foreach (QueryProfile qp in ap.QueryProfiles)
            {
                bool contained = false;

                for (int i = 0; i < QueryProfiles.Count; ++i)
                {
                    if (qp.Query == QueryProfiles[i].Query)
                    {
                        contained = true;
                        QueryProfiles[i] += qp;
                        break;
                    }
                }

                if (!contained)
                {
                    QueryProfiles.Add(qp);
                }
            }

            // Add cycle profile
            CycleProfile += ap.CycleProfile;
        }
    }
}

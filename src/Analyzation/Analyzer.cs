﻿using System;
using System.Collections.Generic;
using GOALLogAnalyser.Analyzation.Agents;

namespace GOALLogAnalyser.Analyzation
{
    /// <summary>
    /// Class that holds an object that is able to analyze records.
    /// </summary>
    public class Analyzer
    {
        private Object _lock = new Object();

        /// <summary>
        /// Gets the profiles.
        /// </summary>
        /// <value>
        /// The profiles.
        /// </value>
        public List<AgentTypeProfile> Profiles { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Analyzer"/> class.
        /// </summary>
        public Analyzer()
        {
            Profiles = new List<AgentTypeProfile>();
        }

        /// <summary>
        /// Adds the specified profile.
        /// </summary>
        /// <param name="profile">The profile.</param>
        public void Add(AgentProfile profile)
        {
            lock (_lock)
            {
                foreach (AgentTypeProfile typeProfile in Profiles)
                {
                    if (typeProfile.Name == profile.Type)
                    {
                        typeProfile.Add(profile);
                        return;
                    }
                }

                AgentTypeProfile newTypeProfile = new AgentTypeProfile(profile.Type);
                newTypeProfile.Add(profile);
                Profiles.Add(newTypeProfile);
            }
        }
    }
}

using System.Collections.Generic;
using System.IO;
using GOALLogAnalyser.Analyzation.Agents;

namespace GOALLogAnalyser.Output
{
    /// <summary>
    /// Class that generates the html and extracts the javascript.
    /// </summary>
    public class SiteGenerator
    {
        /// <summary>
        /// Gets the target directory.
        /// </summary>
        /// <value>
        /// The target directory.
        /// </value>
        public string Directory { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SiteGenerator"/> class.
        /// </summary>
        /// <param name="directory">The target directory.</param>
        public SiteGenerator(string directory)
        {
            Directory = directory;
        }


        /// <summary>
        /// Writes the specified agents to site format.
        /// </summary>
        /// <param name="agents">The agents.</param>
        public void Write(List<AgentTypeProfile> agents)
        {
            System.IO.Directory.CreateDirectory(Directory);

            File.WriteAllText(Directory + "index.html", Properties.Resources.index_template.Replace("~JSON~", Newtonsoft.Json.JsonConvert.SerializeObject(agents)));
            File.WriteAllText(Directory + "bundle.js", Properties.Resources.bundle);
        }
    }
}

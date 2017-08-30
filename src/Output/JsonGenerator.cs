using System.Collections.Generic;
using System.IO;
using GOALLogAnalyser.Analyzation.Agents;

namespace GOALLogAnalyser.Output
{
    /// <summary>
    /// Class that generates json output from in-memory agent data.
    /// </summary>
    public class JsonGenerator
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="JsonGenerator"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public JsonGenerator(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Writes the specified agents to file.
        /// </summary>
        /// <param name="agents">The agents.</param>
        public void Write(List<AgentTypeProfile> agents)
        {
            try
            {
                string dir = Path.GetDirectoryName(FileName);
                if (dir != null && !Directory.Exists(dir))
                    Directory.CreateDirectory(dir);
            }
            catch
            {
                // Do Nothing.
            }

            File.WriteAllText(FileName, Newtonsoft.Json.JsonConvert.SerializeObject(agents));
        }
    }
}

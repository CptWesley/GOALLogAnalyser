using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using GOALLogAnalyser.Analyzation;
using GOALLogAnalyser.Analyzation.Agents;
using GOALLogAnalyser.Output;

namespace GOALLogAnalyser
{
    /// <summary>
    /// Main program that handles input and console printing.
    /// </summary>
    class Program
    {
        private static bool _json, _text, _site;

        static void Main(string[] args)
        {
            Console.Title = "GOALLogAnalyser";
            DateTime start = DateTime.Now;
            List<string> files = CheckFileNames(args);
            Console.WriteLine();
            //List<Agent> agents = GenerateAgents(files);
            Console.WriteLine();
            // List<AgentTypeProfile> agentProfiles = Analyze(agents.ToArray());
            List<AgentTypeProfile> agentProfiles = Analyze(files);
            Console.WriteLine();
            GenerateOutput(agentProfiles);


            Console.WriteLine("Finished in {0} seconds.", (DateTime.Now - start).TotalSeconds.ToString("N3", CultureInfo.InvariantCulture));
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }

        /// <summary>
        /// Checks the for each file if they exist to prevent future errors.
        /// </summary>
        /// <param name="fileNames">The file names.</param>
        /// <returns>A new list of only valid file names.</returns>
        private static List<string> CheckFileNames(string[] fileNames)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("==============================");
            Console.WriteLine("==== Step 1: Finding Files ===");
            Console.WriteLine("==============================");

            int failures = 0;
            List<string> files = new List<string>();
            Regex rgx = new Regex("-logs=(.+)");

            foreach (string file in fileNames)
            {
                Match match = rgx.Match(file);

                if (file == "-json")
                    _json = true;
                else if (file == "-text")
                    _text = true;
                else if (file == "-site")
                    _site = true;
                else if (match.Success)
                {
                    string dir = match.Groups[1].Value;
                    if (Directory.Exists(dir))
                    {
                        foreach (string s in Directory.GetFiles(dir))
                        {
                            files.Add(s);
                            Console.WriteLine("Found: " + s);
                        }
                    }
                }
                else
                {
                    if (File.Exists(file))
                    {
                        files.Add(file);
                        Console.WriteLine("Found: " + file);
                    }
                    else
                    {
                        Console.WriteLine("Missing: " + file);
                        ++failures;
                    }
                }
            }

            if (!_json && !_text && !_site)
                _text = _json = _site = true;

            Console.WriteLine("\nFinished finding files in {0} seconds. Successful: {1} Failures: {2}.",
                (DateTime.Now - start).TotalSeconds.ToString("N3", CultureInfo.InvariantCulture),
                files.Count, failures);

            return files;
        }

        private static List<AgentTypeProfile> Analyze(List<string> files)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("==============================");
            Console.WriteLine("=== Step 2: Analyzing Logs ===");
            Console.WriteLine("==============================");

            Analyzer analyzer = new Analyzer();
            int failures = 0;
            foreach (string file in files)
            {
                Console.WriteLine(file);
                AgentProfile agent;
                try
                {
                    agent = AgentProfile.Create(file);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetType() + ": " + file);
                    ++failures;
                    continue;
                }
                analyzer.Add(agent);
            }

            Console.WriteLine("\nFinished analyzing logs in {0} seconds. Successful: {1} Failures: {2}.",
                (DateTime.Now - start).TotalSeconds.ToString("N3", CultureInfo.InvariantCulture),
                files.Count-failures, failures);

            return analyzer.Profiles;
        }

        /// <summary>
        /// Generates the agents from the supplied files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        private static List<Agent> GenerateAgents(List<string> files)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("==============================");
            Console.WriteLine("==== Step 2: Parsing Logs ====");
            Console.WriteLine("==============================");

            List<Agent> agents = new List<Agent>();
            //Task[] tasks = new Task[files.Count];
            int index = 0;
            int failures = 0;
            for (int i = 0; i < files.Count; ++i)
            {
                int fileIndex = i;
                //tasks[i] = Task.Factory.StartNew(() =>
                //{
                    Agent agent = null;
                    try
                    {
                        agent = Agent.Create(files[fileIndex]);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.GetType().Name + ": " + files[fileIndex]);
                    }

                    if (agent != null)
                    {
                        Console.WriteLine("Parsed: " + files[fileIndex]);
                        agents.Add(agent);
                        ++index;
                    }
                    else
                    {
                        ++failures;
                    }
                //});
            }
            //Task.WaitAll(tasks);

            Console.WriteLine("\nFinished parsing logs in {0} seconds. Successful: {1} Failures: {2}.",
                (DateTime.Now - start).TotalSeconds.ToString("N3", CultureInfo.InvariantCulture),
                index, failures);

            return agents;
        }

        /*
        /// <summary>
        /// Analyzes the specified agents.
        /// </summary>
        /// <param name="agents">The agents.</param>
        /// <returns></returns>
        private static List<AgentTypeProfile> Analyze(Agent[] agents)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("==============================");
            Console.WriteLine("==== Step 3: Analyzing  ======");
            Console.WriteLine("==============================");

            Console.WriteLine("This might take a while...");

            Analyzer analyzer = new Analyzer();

            Task.Factory.StartNew(() => analyzer.Analyze(agents.ToArray()));

            while (!analyzer.Done)
            {
                Console.Write("\rProgress: {0}/{1}.", analyzer.Progress, analyzer.Total);
            }
            Console.Write("\rProgress: {0}/{1}.", analyzer.Progress, analyzer.Total);

            Console.WriteLine();

            if (analyzer.Done)
            {
                Console.WriteLine("\nFinished analyzing agents in {0} seconds.",
                    (DateTime.Now - start).TotalSeconds.ToString("N3", CultureInfo.InvariantCulture));

                return analyzer.Profiles;
            }
            return new List<AgentTypeProfile>();
        }
        */

        /// <summary>
        /// Generates the output.
        /// </summary>
        /// <param name="agents">The agents.</param>
        private static void GenerateOutput(List<AgentTypeProfile> agents)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("==============================");
            Console.WriteLine("==== Step 4: Create Output  ==");
            Console.WriteLine("==============================");

            if (_json)
            {
                JsonGenerator jsonGenerator = new JsonGenerator("output/json/data.json");
                jsonGenerator.Write(agents);
            }
            if (_text)
            {
                TextGenerator textGenerator = new TextGenerator("output/text/");
                textGenerator.Write(agents);
            }
            if (_site)
            {
                SiteGenerator siteGenerator = new SiteGenerator("output/site/");
                siteGenerator.Write(agents);
            }

            Console.WriteLine("\nFinished creating output in {0} seconds.",
                (DateTime.Now - start).TotalSeconds.ToString("N3", CultureInfo.InvariantCulture));
        }
    }
}

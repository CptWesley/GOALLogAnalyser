using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
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
        private static bool _json, _text, _site, _collapsedOutput;
        private static string _outputPath = Directory.GetCurrentDirectory();

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
            Regex logsRgx = new Regex("-logs=(.+)");
            Regex outputRgx = new Regex("-output=(.+)");

            foreach (string file in fileNames)
            {
                Match logsMatch = logsRgx.Match(file);
                Match outputMatch = outputRgx.Match(file);

                if (file == "-json")
                    _json = true;
                else if (file == "-text")
                    _text = true;
                else if (file == "-site")
                    _site = true;
                else if (file == "-collapsedoutput")
                    _collapsedOutput = true;
                else if (logsMatch.Success)
                {
                    string dir = logsMatch.Groups[1].Value;
                    if (Directory.Exists(dir))
                    {
                        foreach (string s in Directory.GetFiles(dir))
                        {
                            files.Add(s);
                            Console.WriteLine("Found: " + s);
                        }
                    }
                }
                else if (outputMatch.Success)
                {
                    string dir = outputMatch.Groups[1].Value;
                    if (Directory.Exists(dir))
                    {
                        _outputPath = dir;
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

        /// <summary>
        /// Analyzes the specified files.
        /// </summary>
        /// <param name="files">The files.</param>
        /// <returns></returns>
        private static List<AgentTypeProfile> Analyze(List<string> files)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("==============================");
            Console.WriteLine("=== Step 2: Analyzing Logs ===");
            Console.WriteLine("==============================");

            Analyzer analyzer = new Analyzer();
            int failures = 0;
            Task[] tasks = files.Select(file => Task.Run(() =>
            {
                try
                {
                    analyzer.Add(AgentProfile.Create(file));
                    Console.WriteLine("Parsed: " +  file);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.GetType() + ": " + file);
                    Interlocked.Increment(ref failures);
                }
            })).ToArray();
            Task.WaitAll(tasks);

            Console.WriteLine("\nFinished analyzing logs in {0} seconds. Successful: {1} Failures: {2}.",
                (DateTime.Now - start).TotalSeconds.ToString("N3", CultureInfo.InvariantCulture),
                files.Count-failures, failures);

            return analyzer.Profiles;
        }

        /// <summary>
        /// Generates the output.
        /// </summary>
        /// <param name="agents">The agents.</param>
        private static void GenerateOutput(List<AgentTypeProfile> agents)
        {
            DateTime start = DateTime.Now;
            Console.WriteLine("==============================");
            Console.WriteLine("==== Step 3: Create Output  ==");
            Console.WriteLine("==============================");

            if (_json)
            {
                string path;
                if (_collapsedOutput)
                    path = Path.Combine(_outputPath, "data.json");
                else
                    path = Path.Combine(_outputPath, "output/json/data.json");

                JsonGenerator jsonGenerator = new JsonGenerator(path);
                jsonGenerator.Write(agents);
            }
            if (_text)
            {
                string path;
                if (_collapsedOutput)
                    path = _outputPath;
                else
                    path = Path.Combine(_outputPath, "output/text/");

                TextGenerator textGenerator = new TextGenerator(path);
                textGenerator.Write(agents);
            }
            if (_site)
            {
                string path;
                if (_collapsedOutput)
                    path = _outputPath;
                else
                    path = Path.Combine(_outputPath, "output/site/");

                SiteGenerator siteGenerator = new SiteGenerator(path);
                siteGenerator.Write(agents);
            }

            Console.WriteLine("\nFinished creating output in {0} seconds.",
                (DateTime.Now - start).TotalSeconds.ToString("N3", CultureInfo.InvariantCulture));
        }
    }
}

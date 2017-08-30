using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace GOALLogAnalyser.Analyzation
{
    /// <summary>
    /// Static class containing methods that allow parsing record messages into usable data.
    /// </summary>
    public static class RecordMessageParser
    {
        private static Dictionary<uint, string> _types = new Dictionary<uint, string>
        {
            { RecordMessageType.ModuleEntryType, RecordMessageType.ModuleEntryPattern },
            { RecordMessageType.ModuleExitType, RecordMessageType.ModuleExitPattern },
            { RecordMessageType.CycleStatisticsType, RecordMessageType.CycleStatisticsPattern }
        };

        /// <summary>
        /// Parses the specified message.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <returns>The message type and the message arguments.</returns>
        public static Tuple<uint, string[]> Parse(string message)
        {
            uint type = 0;
            string[] args = new string[0];

            foreach (KeyValuePair<uint, string> pair in _types)
            {
                Regex rgx = new Regex(pair.Value);
                Match m = rgx.Match(message);
                if (m.Success)
                {
                    type = pair.Key;

                    args = new string[m.Groups.Count - 1];
                    for (int i = 0; i < args.Length; ++i)
                    {
                        args[i] = m.Groups[i + 1].Value;
                    }

                    break;
                }
            }

            return new Tuple<uint, string[]>(type, args);
        }
    }
}

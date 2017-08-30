using System.Collections.Generic;
using System.Xml;
using System.Xml.Linq;
using GOALLogAnalyser.Exceptions;

namespace GOALLogAnalyser.Parsing
{
    /// <summary>
    /// Class containing functionality of parsing a log.
    /// </summary>
    public class LogParser
    {
        /// <summary>
        /// Gets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="LogParser"/> class.
        /// </summary>
        /// <param name="fileName">Name of the file.</param>
        public LogParser(string fileName)
        {
            FileName = fileName;
        }

        /// <summary>
        /// Parses the file.
        /// </summary>
        /// <returns>The records contained in the file.</returns>
        /// <exception cref="GOALLogAnalyser.Exceptions.InvalidFileContentException">
        /// </exception>
        public List<Record> Parse()
        {
            List<Record> records = new List<Record>();

            XmlReaderSettings settings = new XmlReaderSettings
            {
                DtdProcessing = DtdProcessing.Parse
            };

            using (XmlReader reader = XmlReader.Create(FileName, settings))
            {
                try
                {
                    while (reader.Read())
                    {
                        if (reader.IsStartElement() && reader.Name == "record")
                        {
                            XElement el = (XElement) XNode.ReadFrom(reader);
                            Record record;
                            try
                            {
                                record = Record.Parse(el);
                            }
                            catch
                            {
                                throw new InvalidFileContentException();
                            }
                            if (record != null)
                            {
                                records.Add(record);
                            }
                        }
                    }
                }
                catch
                {
                    throw new InvalidFileContentException();
                }
            }

            if (records.Count == 0)
            {
                throw new InvalidFileContentException();
            }

            return records;
        }
    }
}

using System.Xml.Linq;

namespace GOALLogAnalyser.Parsing
{
    /// <summary>
    /// Class containing the data of a record.
    /// </summary>
    public class Record
    {
        /// <summary>
        /// Gets or sets the time.
        /// </summary>
        /// <value>
        /// The time.
        /// </value>
        public long Time { get; set; }
        /// <summary>
        /// Gets or sets the sequence number.
        /// </summary>
        /// <value>
        /// The sequence number.
        /// </value>
        public long Sequence { get; set; }
        /// <summary>
        /// Gets or sets the thread number.
        /// </summary>
        /// <value>
        /// The thread number.
        /// </value>
        public int Thread { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>
        /// The message.
        /// </value>
        public string Message { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Record"/> class.
        /// </summary>
        public Record()
        {
            Time = -1;
            Sequence = -1;
            Thread = -1;
            Message = "";
        }

        /// <summary>
        /// Determines whether this instance has a specified timestamp.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has a specified timestamp; otherwise, <c>false</c>.
        /// </returns>
        public bool HasTime()
        {
            return Time != -1;
        }

        /// <summary>
        /// Determines whether this instance has a sequence number.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has a sequence number; otherwise, <c>false</c>.
        /// </returns>
        public bool HasSequence()
        {
            return Sequence != -1;
        }

        /// <summary>
        /// Determines whether this instance has a thread number.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has a thread number; otherwise, <c>false</c>.
        /// </returns>
        public bool HasThread()
        {
            return Thread != -1;
        }

        /// <summary>
        /// Determines whether this instance has a message.
        /// </summary>
        /// <returns>
        ///   <c>true</c> if this instance has a message; otherwise, <c>false</c>.
        /// </returns>
        public bool HasMessage()
        {
            return !string.IsNullOrEmpty(Message);
        }

        /// <summary>
        /// Parses the specified <see cref="XElement"/>.
        /// </summary>
        /// <param name="el">The <see cref="XElement"/>.</param>
        /// <returns></returns>
        public static Record Parse(XElement el)
        {
            long time = -1;
            long sequence = -1;
            int thread = -1;
            string message = "";

            foreach (XElement child in el.Descendants())
            {
                switch (child.Name.ToString())
                {
                    case "message":
                        message = child.Value;
                        break;
                    case "thread":
                        thread = int.Parse(child.Value);
                        break;
                    case "millis":
                        time = long.Parse(child.Value);
                        break;
                    case "sequence":
                        sequence = long.Parse(child.Value);
                        break;
                }
            }

            return new Record
            {
                Time = time,
                Sequence = sequence,
                Thread = thread,
                Message = message
            };
        }
    }
}

using System;

namespace GOALLogAnalyser.Exceptions
{
    /// <summary>
    /// Exception class used when the file name could not be proper parsed.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidFileNameException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFileNameException"/> class.
        /// </summary>
        public InvalidFileNameException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFileNameException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidFileNameException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFileNameException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public InvalidFileNameException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

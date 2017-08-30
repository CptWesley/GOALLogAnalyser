using System;

namespace GOALLogAnalyser.Exceptions
{
    /// <summary>
    /// Exception class used when the file content is not a proper GOAL log.
    /// </summary>
    /// <seealso cref="System.Exception" />
    public class InvalidFileContentException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFileContentException"/> class.
        /// </summary>
        public InvalidFileContentException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFileContentException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public InvalidFileContentException(string message) : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidFileContentException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="inner">The inner exception.</param>
        public InvalidFileContentException(string message, Exception inner) : base(message, inner)
        {
        }
    }
}

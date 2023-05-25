using System;

namespace WZIMopoly.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a type is invalid.
    /// </summary>
    internal class InvalidTypeException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InvalidTypeException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message of the exception.
        /// </param>
        public InvalidTypeException(string message) : base(message) { }
    }
}

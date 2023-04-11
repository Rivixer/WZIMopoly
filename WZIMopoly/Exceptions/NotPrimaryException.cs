using System;


namespace WZIMopoly.Exceptions
{
    /// <summary>
    /// Represents an exception that is thrown when a controller is not primary.
    /// </summary>
    /// <remarks>
    /// This exception is typically thrown when a method or operation
    /// requires a primary controller,<br/>
    /// but the current controller is not designated as the primary one.
    /// </remarks>
    internal class NotPrimaryException : Exception
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NotPrimaryException"/> class.
        /// </summary>
        /// <param name="message">
        /// The message of the exception.
        /// </param>
        internal NotPrimaryException(string message) : base(message) { }
    }
}


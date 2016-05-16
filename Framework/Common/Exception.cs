using System;

namespace Framework.Common
{
    public class RLException : Exception
    {

        /// <summary>
        /// Initializes a new instance of the <see cref="RLException"/> class.
        /// </summary>
        public RLException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RLException"/> class.
        /// </summary>
        /// <param name="message">The message that describes the error.</param>
        public RLException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RLException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        /// <param name="ex">The ex.</param>
        public RLException(string message, Exception ex)
            : base(message, ex)
        {
        }
    }
}

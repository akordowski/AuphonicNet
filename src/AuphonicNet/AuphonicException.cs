using System;
using System.Net;

namespace AuphonicNet
{
    /// <summary>
    /// Represents errors that occur during application execution.
    /// </summary>
    public class AuphonicException : Exception
    {
        #region Public Properties
        /// <summary>
        /// Gets the HTTP response content.
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Gets the error code.
        /// </summary>
        public string ErrorCode { get; }

        /// <summary>
        /// Gets the error message.
        /// </summary>
        public string ErrorMessage { get; }

        /// <summary>
        /// Gets the HTTP status code.
        /// </summary>
        public HttpStatusCode StatusCode { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="AuphonicException"/> class with a specified error message.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="content">The HTTP response content.</param>
        public AuphonicException(string errorCode, string errorMessage, HttpStatusCode statusCode, string content)
            : this(errorCode, errorMessage, statusCode, content, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AuphonicException"/> class with a specified error message.
        /// </summary>
        /// <param name="errorCode">The error code.</param>
        /// <param name="errorMessage">The error message.</param>
        /// <param name="statusCode">The HTTP status code.</param>
        /// <param name="content">The HTTP response content.</param>
        /// <param name="innerException">The inner exception.</param>
        public AuphonicException(string errorCode, string errorMessage, HttpStatusCode statusCode, string content, Exception innerException)
            : base(errorMessage, innerException)
        {
            ErrorCode = errorCode;
            ErrorMessage = errorMessage;
            Content = content;
            StatusCode = statusCode;
        }
        #endregion
    }
}
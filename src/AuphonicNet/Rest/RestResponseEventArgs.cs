using RestSharp;
using System;

namespace AuphonicNet.Rest
{
    /// <summary>
    /// Represents a class that contain event data.
    /// </summary>
    public class RestResponseEventArgs : EventArgs
    {
        #region Public Properties
        /// <summary>
        /// Gets the response object.
        /// </summary>
        public IRestResponse Response { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RestResponseEventArgs"/> class.
        /// </summary>
        /// <param name="response">The response object.</param>
        public RestResponseEventArgs(IRestResponse response)
        {
            Response = response;
        }
        #endregion
    }
}
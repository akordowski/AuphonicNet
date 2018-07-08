using RestSharp;
using System;

namespace AuphonicNet.Rest
{
    /// <summary>
    /// Represents a class that contain event data.
    /// </summary>
    public class RestRequestEventArgs : EventArgs
    {
        #region Public Properties
        /// <summary>
        /// Gets the request object.
        /// </summary>
        public IRestRequest Request { get; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequestEventArgs"/> class.
        /// </summary>
        /// <param name="request">The request object.</param>
        public RestRequestEventArgs(IRestRequest request)
        {
            Request = request;
        }
        #endregion
    }
}
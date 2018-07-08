using AuphonicNet.Json;
using RestSharp;
using System;

namespace AuphonicNet.Rest
{
    /// <summary>
    /// Provides a <see cref="RestRequest"/> class.
    /// </summary>
    internal class RestRequest : RestSharp.RestRequest
    {
        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequest"/> class.
        /// </summary>
        public RestRequest()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequest"/> class.
        /// </summary>
        /// <param name="method">Method to use for this request.</param>
        public RestRequest(Method method)
            : base(method)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequest"/> class.
        /// </summary>
        /// <param name="resource">Resource to use for this request.</param>
        public RestRequest(string resource)
            : base(resource)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequest"/> class.
        /// </summary>
        /// <param name="resource">Resource to use for this request.</param>
        /// <param name="method">Method to use for this request.</param>
        public RestRequest(string resource, Method method)
            : base(resource, method)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequest"/> class.
        /// </summary>
        /// <param name="resource">Resource to use for this request.</param>
        public RestRequest(Uri resource)
            : base(resource)
        {
            IntializeJsonSerializer();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RestRequest"/> class.
        /// </summary>
        /// <param name="resource">Resource to use for this request.</param>
        /// <param name="method">Method to use for this request.</param>
        public RestRequest(Uri resource, Method method)
            : base(resource, method)
        {
            IntializeJsonSerializer();
        }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Intializes the serializer.
        /// </summary>
        protected void IntializeJsonSerializer()
        {
            JsonSerializer = new NewtonsoftJsonSerializer();
        }
        #endregion
    }
}
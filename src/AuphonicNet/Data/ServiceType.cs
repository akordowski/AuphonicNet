using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a service type.
    /// </summary>
    public class ServiceType
    {
        #region Public Properties
        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        [JsonProperty]
        public string DisplayName { get; internal set; }

        /// <summary>
        /// Gets a collection of key/value pairs that provide additional parameters supported by
        /// the service.
        /// </summary>
        [JsonProperty]
        public Dictionary<string, Parameter> Parameters { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceType"/> class.
        /// </summary>
        internal ServiceType()
        {
        }
        #endregion

        #region Public Override Methods
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return DisplayName;
        }
        #endregion
    }
}
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a parameter for a service.
    /// </summary>
    public class Parameter
    {
        #region Public Properties
        /// <summary>
        /// Gets the default value of the parameter.
        /// </summary>
        [JsonProperty]
        public string DefaultValue { get; internal set; }

        /// <summary>
        /// Gets the name of the parameter.
        /// </summary>
        [JsonProperty]
        public string DisplayName { get; internal set; }

        /// <summary>
        /// Gets a list of options for the service parameter.
        /// </summary>
        [JsonProperty]
        public List<Option> Options { get; internal set; }

        /// <summary>
        /// Gets the type of the parameter.
        /// </summary>
        [JsonProperty]
        public string Type { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Parameter"/> class.
        /// </summary>
        internal Parameter()
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
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a audio algorithm.
    /// </summary>
    public class Algorithm
    {
        #region Public Properties
        /// <summary>
        /// The name of the algorithm to which this algorithm belongs to.
        /// </summary>
        [JsonProperty]
        public string BelongsTo { get; internal set; }

        /// <summary>
        /// Gets the controls default value.
        /// </summary>
        [JsonProperty]
        public string DefaultValue { get; internal set; }

        /// <summary>
        /// Gets the description of the algorithm.
        /// </summary>
        [JsonProperty]
        public string Description { get; internal set; }

        /// <summary>
        /// Gets the name of the algorithm.
        /// </summary>
        [JsonProperty]
        public string DisplayName { get; internal set; }

        /// <summary>
        /// Gets a list of options for the algorithm.
        /// </summary>
        [JsonProperty]
        public List<Option> Options { get; internal set; }

        /// <summary>
        /// Gets the controls type for the algorithm (eg. checkbox, select).
        /// </summary>
        [JsonProperty]
        public string Type { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Algorithm"/> class.
        /// </summary>
        internal Algorithm()
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
            return $"{DisplayName} = {DefaultValue}";
        }
        #endregion
    }
}
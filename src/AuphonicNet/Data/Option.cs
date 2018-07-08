using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a option.
    /// </summary>
    public class Option
    {
        #region Public Properties
        /// <summary>
        /// Gets the name of the option.
        /// </summary>
        [JsonProperty]
        public string DisplayName { get; internal set; }

        /// <summary>
        /// Gets the value of the option.
        /// </summary>
        [JsonProperty]
        public string Value { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Option"/> class.
        /// </summary>
        internal Option()
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
            return $"{DisplayName} = {Value}";
        }
        #endregion
    }
}
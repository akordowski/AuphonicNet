using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents credits informations used for a production.
    /// </summary>
    public class Credits
    {
        #region Public Properties
        /// <summary>
        /// Gets the combined credits (onetime and recurring).
        /// </summary>
        [JsonProperty]
        public decimal Combined { get; internal set; }

        /// <summary>
        /// Gets the onetime credits.
        /// </summary>
        [JsonProperty]
        public decimal Onetime { get; internal set; }

        /// <summary>
        /// Gets the recurring credits.
        /// </summary>
        [JsonProperty]
        public decimal Recurring { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Credits"/> class.
        /// </summary>
        internal Credits()
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
            return $"Combined = {Combined}; Onetime = {Onetime}; Recurring = {Recurring}";
        }
        #endregion
    }
}
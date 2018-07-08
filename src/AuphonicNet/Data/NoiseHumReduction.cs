using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a noise and/or hum reduction segment.
    /// </summary>
    public class NoiseHumReduction
    {
        #region Public Properties
        /// <summary>
        /// Gets the dehum level.
        /// </summary>
        [JsonConverter(typeof(NoiseHumReductionConverter))]
        [JsonProperty]
        public long? Dehum { get; internal set; }

        /// <summary>
        /// Gets the denoise level.
        /// </summary>
        [JsonConverter(typeof(NoiseHumReductionConverter))]
        [JsonProperty]
        public long? Denoise { get; internal set; }

        /// <summary>
        /// Gets the start of the segment as timestring (in HH:MM:SS.mmm).
        /// </summary>
        [JsonProperty]
        public string Start { get; internal set; }

        /// <summary>
        /// Gets the start of the segment in seconds.
        /// </summary>
        [JsonProperty]
        public decimal StartSec { get; internal set; }

        /// <summary>
        /// Gets the end of the segment as timestring (in HH:MM:SS.mmm).
        /// </summary>
        [JsonProperty]
        public string Stop { get; internal set; }

        /// <summary>
        /// Gets the end of the segment in seconds.
        /// </summary>
        [JsonProperty]
        public decimal StopSec { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="NoiseHumReduction"/> class.
        /// </summary>
        internal NoiseHumReduction()
        {
        }
        #endregion
    }
}
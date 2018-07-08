using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents processing statistics about audio levels, loudness, gain changes, peaks etc.
    /// </summary>
    public class LevelStatistics
    {
        #region Public Properties
        /// <summary>
        /// Gets the leveler gain max.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level GainMax { get; internal set; }

        /// <summary>
        /// Gets the leveler gain mean.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level GainMean { get; internal set; }

        /// <summary>
        /// Gets the leveler gain min.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level GainMin { get; internal set; }

        /// <summary>
        /// Gets the programme loudness.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level Loudness { get; internal set; }

        /// <summary>
        /// Gets the loudness range (LRA).
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level Lra { get; internal set; }

        /// <summary>
        /// Gets the max momentary loudness.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level MaxMomentary { get; internal set; }

        /// <summary>
        /// Gets the max shortterm loudness.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level MaxShortterm { get; internal set; }

        /// <summary>
        /// Gets the background level mean.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level NoiseLevel { get; internal set; }

        /// <summary>
        /// Gets the max peak level.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level Peak { get; internal set; }

        /// <summary>
        /// Gets the signal level mean.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level SignalLevel { get; internal set; }

        /// <summary>
        /// Gets the SNR mean.
        /// </summary>
        [JsonConverter(typeof(LevelConverter))]
        [JsonProperty]
        public Level Snr { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="LevelStatistics"/> class.
        /// </summary>
        internal LevelStatistics()
        {
        }
        #endregion
    }
}
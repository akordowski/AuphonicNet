using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents the audio format statistics.
    /// </summary>
    public class Format
    {
        #region Public Properties
        /// <summary>
        /// Gets the audio bitrate.
        /// </summary>
        [JsonProperty]
        public decimal Bitrate { get; internal set; }

        /// <summary>
        /// Gets the audio channels.
        /// </summary>
        [JsonProperty]
        public int Channels { get; internal set; }

        /// <summary>
        /// Gets the audio file format.
        /// </summary>
        [JsonProperty("format")]
        public string FileFormat { get; internal set; }

        /// <summary>
        /// Gets the audio length in seconds.
        /// </summary>
        [JsonProperty]
        public decimal LengthSec { get; internal set; }

        /// <summary>
        /// Gets the audio sample rate.
        /// </summary>
        [JsonProperty("samplerate")]
        public int SampleRate { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Format"/> class.
        /// </summary>
        internal Format()
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
            return FileFormat;
        }
        #endregion
    }
}
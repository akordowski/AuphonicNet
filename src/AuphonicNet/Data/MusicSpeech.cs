using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a music or speech segment.
    /// </summary>
    public class MusicSpeech
    {
        #region Public Properties
        /// <summary>
        /// Gets the segment label (music, speech).
        /// </summary>
        [JsonProperty]
        public string Label { get; internal set; }

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
        /// Initializes a new instance of the <see cref="MusicSpeech"/> class.
        /// </summary>
        internal MusicSpeech()
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
            return $"{Label} (Start = {Start}; Stop = {Stop})";
        }
        #endregion
    }
}
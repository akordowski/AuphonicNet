using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents the audio processing statistics.
    /// </summary>
    public class Statistics
    {
        #region Public Properties
        /// <summary>
        /// Gets the audio format.
        /// </summary>
        [JsonProperty]
        public Format Format { get; internal set; }

        /// <summary>
        /// Gets processing statistics about audio levels, loudness, gain changes, peaks etc. of
        /// input and output files.
        /// </summary>
        [JsonProperty]
        public Levels Levels { get; internal set; }

        /// <summary>
        /// Gets a list of music and speech segments within the audio file (> 20 sec).
        /// </summary>
        [JsonProperty]
        public List<MusicSpeech> MusicSpeech { get; internal set; }

        /// <summary>
        /// Gets a list of segments with noise and/or hum reduction.
        /// </summary>
        [JsonProperty]
        public List<NoiseHumReduction> NoiseHumReduction { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Statistics"/> class.
        /// </summary>
        internal Statistics()
        {
        }
        #endregion
    }
}
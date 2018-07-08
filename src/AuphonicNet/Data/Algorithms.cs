using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents the preset/production audio algorithms settings.
    /// </summary>
    public class Algorithms
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets a value that indicates whether the automatic Noise and Hum reduction
        /// algorithm is used. Classifies regions with different backgrounds and automatically
        /// removes noise and hum in each region. For details and examples see
        /// <a href="https://auphonic.com/help/algorithms/singletrack.html#noise-and-hiss-reduction">Noise and Hiss Reduction</a> and
        /// <a href="https://auphonic.com/help/algorithms/singletrack.html#hum-reduction">Hum Reduction</a>.
        /// </summary>
        [JsonProperty]
        public bool Denoise { get; set; }

        /// <summary>
        /// Gets or sets a value for the maximum noise reduction in <em>dB</em>. Higher values
        /// remove more noise. <strong>IMPORTANT:</strong> please use Auto if you are not sure,
        /// otherwise music segments might be destroyed!
        /// </summary>
        [JsonProperty("denoiseamount")]
        public int DenoiseAmount { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the High-Pass Filtering algorithm is used.
        /// Classifies the lowest wanted signal (male/female speech, base in music, etc.) and
        /// <a href="https://auphonic.com/help/algorithms/singletrack.html#adaptive-filtering">adaptively filters</a>
        /// unnecessary/disturbing low frequencies in each audio segment.
        /// </summary>
        [JsonProperty("hipfilter")]
        public bool HipFilter { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the adaptive leveler algorithm is used.
        /// Corrects level differences within one file between speakers, music and speech, etc. to
        /// achieve a balanced overall loudness. For details and examples see 
        /// <a href="https://auphonic.com/help/algorithms/singletrack.html#adaptive-leveler">Adaptive Leveler</a>.
        /// </summary>
        [JsonProperty]
        public bool Leveler { get; set; }

        /// <summary>
        /// Gets or sets a value for the loudness target in <em>LUFS</em> for
        /// <em><a href="https://auphonic.com/help/algorithms/singletrack.html#global-loudness-normalization-and-true-peak-limiter">Loudness Normalization</a></em>.
        /// Higher values result in louder audio outputs.
        /// </summary>
        [JsonProperty("loudnesstarget")]
        public int LoudnessTarget { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the Global Loudness Normalization with True
        /// Peak Limiter algorithm is used. Adjusts the global, overall loudness to the specified
        /// <em><a href="https://auphonic.com/help/algorithms/singletrack.html#global-loudness-normalization-and-true-peak-limiter">Loudness Target</a></em>,
        /// so that all processed files have a similar average loudness.
        /// </summary>
        [JsonProperty("normloudness")]
        public bool NormLoudness { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Algorithms"/> class.
        /// </summary>
        public Algorithms()
        {
        }
        #endregion
    }
}
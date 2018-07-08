using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a multi input file.
    /// </summary>
    public class MultiInputFile
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the audio algorithms settings.
        /// </summary>
        [JsonProperty]
        public Algorithms Algorithms { get; set; }

        /// <summary>
        /// Gets or sets the ID of the input file.
        /// </summary>
        [JsonProperty]
        public string Id { get; set; }

        /// <summary>
        /// Gets the input audio bitrate.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public int InputBitrate { get; internal set; }

        /// <summary>
        /// Gets the channels of the input audio.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public int InputChannels { get; internal set; }

        /// <summary>
        /// Gets or sets the filename of the input audio.
        /// </summary>
        [JsonProperty]
        public string InputFile { get; set; }

        /// <summary>
        /// Gets the file type of input audio.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string InputFiletype { get; internal set; }

        /// <summary>
        /// Gets the input audio length in seconds.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public decimal InputLength { get; internal set; }

        /// <summary>
        /// Gets the input audio samplerate.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public int InputSamplerate { get; internal set; }

        /// <summary>
        /// Gets or sets the offset of the input audio.
        /// </summary>
        [JsonProperty]
        public double Offset { get; set; }

        /// <summary>
        /// Gets or sets the service UUID.
        /// </summary>
        [JsonProperty]
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets the type of the input audio.
        /// </summary>
        [JsonProperty]
        public string Type { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="InputFile"/> class.
        /// </summary>
        public MultiInputFile()
        {
        }
        #endregion
    }
}
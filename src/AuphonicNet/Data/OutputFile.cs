using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a output file.
    /// </summary>
    public class OutputFile
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets combined bitrate of all channels of the output file.
        /// </summary>
        [JsonProperty]
        public int Bitrate { get; set; }

        /// <summary>
        /// Gets a 128 Bit MD5 checksum of the output file.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string Checksum { get; internal set; }

        /// <summary>
        /// Gets an HTTP URL on Auphonic servers to download the output file.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string DownloadUrl { get; internal set; }

        /// <summary>
        /// Gets or sets the filename extension of the output file.
        /// </summary>
        [JsonProperty]
        public string Ending { get; set; }

        /// <summary>
        /// Gets or sets the filename of the output file.
        /// </summary>
        [JsonProperty]
        public string Filename { get; set; }

        /// <summary>
        /// Gets or sets the format of the output file (e.g. mp3, m4a).
        /// </summary>
        [JsonProperty]
        public string Format { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to force a mono mixdown of the output file.
        /// </summary>
        [JsonProperty]
        public bool MonoMixdown { get; set; }

        /// <summary>
        /// Gets or sets referenced outgoing file transfer services.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OutgoingService> OutgoingServices { get; set; }

        /// <summary>
        /// Gets the size of the output file in bytes.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public int Size { get; internal set; }

        /// <summary>
        /// Gets the size of the output file as human readable string.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string SizeString { get; internal set; }

        /// <summary>
        /// Gets or sets a value that indicates whether to split the audio in one file per chapter,
        /// if chapters are provided.
        /// </summary>
        /// <remarks>All filenames will be appended with the chapter number and packed into one ZIP
        /// output file.</remarks>
        [JsonProperty]
        public bool SplitOnChapters { get; set; }

        /// <summary>
        /// Gets or sets the suffix for filename generation of the output file.
        /// </summary>
        /// <value>Default <strong>null</strong> for automatic suffix.</value>
        [JsonProperty]
        public string Suffix { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputFile"/> class.
        /// </summary>
        public OutputFile()
        {
        }
        #endregion
    }
}
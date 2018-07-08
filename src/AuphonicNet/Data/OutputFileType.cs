using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a output file type.
    /// </summary>
    public class OutputFileType
    {
        #region Public Properties
        /// <summary>
        /// Gets a list of bitrates supported by the output file.
        /// </summary>
        [JsonProperty]
        public List<string> Bitrates { get; internal set; }

        /// <summary>
        /// Gets a list of string representations of bitrates supported by the output file.
        /// </summary>
        [JsonProperty]
        public List<string> BitrateStrings { get; internal set; }

        /// <summary>
        /// Gets the default bitrate of the output file.
        /// </summary>
        [JsonProperty]
        public string DefaultBitrate { get; internal set; }

        /// <summary>
        /// Gets the name of the output file.
        /// </summary>
        [JsonProperty]
        public string DisplayName { get; internal set; }

        /// <summary>
        /// Gets a list of file endings supported by the output file.
        /// </summary>
        [JsonProperty]
        public List<string> Endings { get; internal set; }

        /// <summary>
        /// Gets the type of the output file.
        /// </summary>
        [JsonProperty]
        public string Type { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OutputFileType"/> class.
        /// </summary>
        internal OutputFileType()
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
            return DisplayName;
        }
        #endregion
    }
}
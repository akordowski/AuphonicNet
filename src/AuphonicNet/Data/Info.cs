using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents combined informations for supported values.
    /// </summary>
    public class Info
    {
        #region Public Properties
        /// <summary>
        /// Gets all supported audio algorithms.
        /// </summary>
        [JsonProperty]
        public Dictionary<string, Algorithm> Algorithms { get; internal set; }

        /// <summary>
        /// Gets all supported file endings.
        /// </summary>
        [JsonProperty]
        public Dictionary<string, List<string>> FileEndings { get; internal set; }

        /// <summary>
        /// Gets all supported output file formats.
        /// </summary>
        [JsonProperty]
        public Dictionary<string, OutputFileType> OutputFiles { get; internal set; }

        /// <summary>
        /// Gets all status codes of an audio production.
        /// </summary>
        [JsonConverter(typeof(DictionaryEnumKeyConverter<ProductionStatus>))]
        [JsonProperty]
        public Dictionary<ProductionStatus, string> ProductionStatus { get; internal set; }

        /// <summary>
        /// Gets all supported external services.
        /// </summary>
        [JsonProperty]
        public Dictionary<string, ServiceType> ServiceTypes { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Info"/> class.
        /// </summary>
        internal Info()
        {
        }
        #endregion
    }
}
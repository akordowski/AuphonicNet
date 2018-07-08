using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents processing statistics of input and output files.
    /// </summary>
    public class Levels
    {
        #region Public Properties
        /// <summary>
        /// Gets the input files processing statistics.
        /// </summary>
        [JsonProperty]
        public LevelStatistics Input { get; internal set; }

        /// <summary>
        /// Gets the output files processing statistics.
        /// </summary>
        [JsonProperty]
        public LevelStatistics Output { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Levels"/> class.
        /// </summary>
        internal Levels()
        {
        }
        #endregion
    }
}
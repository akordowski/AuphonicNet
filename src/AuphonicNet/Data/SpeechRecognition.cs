using Newtonsoft.Json;
using System;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents speech recognition information.
    /// </summary>
    public class SpeechRecognition
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets keywords for the speech recognition.
        /// </summary>
        [JsonProperty]
        public string[] Keywords { get; set; }

        /// <summary>
        /// Gets or sets language for the speech recognition (e.g. en-US).
        /// </summary>
        [JsonProperty]
        public string Language { get; set; }

        /// <summary>
        /// Gets or sets the type of the speech recognition service.
        /// </summary>
        [JsonProperty]
        public string Type { get; set; }

        /// <summary>
        /// Gets or sets the speech recognition service UUID.
        /// </summary>
        [JsonProperty]
        public string Uuid { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="SpeechRecognition"/> class.
        /// </summary>
        internal SpeechRecognition()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpeechRecognition"/> class.
        /// </summary>
        /// <param name="uuid">The speech recognition service UUID.</param>
        /// <param name="language">The language for the speech recognition.</param>
        /// <param name="keywords">Keywords for the speech recognition.</param>
        /// <exception cref="ArgumentException"><em>uuid</em> or <em>language</em> is an empty
        /// string or contains whitespace characters, or <em>keywords</em> is an empty array.</exception>
        /// <exception cref="ArgumentNullException"><em>uuid</em>, <em>language</em> or
        /// <em>keywords</em> is <strong>null</strong>.</exception>
        public SpeechRecognition(string uuid, string language, string[] keywords)
        {
            Precondition.IsNotNullOrWhiteSpace(uuid, nameof(uuid));
            Precondition.IsNotNullOrWhiteSpace(language, nameof(language));
            Precondition.IsNotNullOrEmpty(keywords, nameof(keywords));

            Uuid = uuid;
            Language = language;
            Keywords = keywords;
        }
        #endregion
    }
}
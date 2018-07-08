using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a preset for a production.
    /// </summary>
    public class Preset
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the audio algorithms settings.
        /// </summary>
        [JsonProperty]
        public Algorithms Algorithms { get; set; }

        /// <summary>
        /// Gets the preset creation time.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public DateTime CreationTime { get; internal set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the production is multitrack.
        /// </summary>
        [JsonProperty]
        public bool IsMultitrack { get; set; }

        /// <summary>
        /// Gets or sets metadata.
        /// </summary>
        [JsonProperty]
        public Metadata Metadata { get; set; }

        /// <summary>
        /// Gets or sets multi input files.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<MultiInputFile> MultiInputFiles { get; set; }

        /// <summary>
        /// Gets or sets outgoing services.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OutgoingService> OutgoingServices { get; set; }

        /// <summary>
        /// Gets or sets the output file basename.
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string OutputBasename { get; set; }

        /// <summary>
        /// Gets or sets output files.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<OutputFile> OutputFiles { get; set; }

        /// <summary>
        /// Gets or sets the preset name.
        /// </summary>
        [JsonProperty]
        public string PresetName { get; set; }

        /// <summary>
        /// Gets or sets the speech recognition.
        /// </summary>
        [JsonProperty]
        public SpeechRecognition SpeechRecognition { get; set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        [JsonProperty]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets the preset UUID.
        /// </summary>
        [JsonProperty]
        public string Uuid { get; internal set; }

        /// <summary>
        /// Gets or sets the webhook URL.
        /// </summary>
        /// <remarks>For details see <a href="https://auphonic.com/help/api/webhook.html">Webhooks</a>.</remarks>
        [JsonProperty]
        public string Webhook { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Preset"/> class.
        /// </summary>
        internal Preset()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Preset"/> class.
        /// </summary>
        /// <param name="presetName">The preset name.</param>
        /// <exception cref="ArgumentException"><em>presetName</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>presetName</em> is <strong>null</strong>.</exception>
        public Preset(string presetName)
        {
            Precondition.IsNotNullOrWhiteSpace(presetName, nameof(presetName));

            PresetName = presetName;
        }
        #endregion

        #region Public Override Methods
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return PresetName;
        }
        #endregion
    }
}
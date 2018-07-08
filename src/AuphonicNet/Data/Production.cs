using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a production.
    /// </summary>
    public class Production
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the audio algorithms settings.
        /// </summary>
        [JsonProperty]
        public Algorithms Algorithms { get; set; }

        /// <summary>
        /// Gets the input audio bitrate.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public decimal? Bitrate { get; internal set; }

        /// <summary>
        /// Gets or sets a value that indicates whether it's allowed to change the production
        /// (e.g. not during audio processing).
        /// </summary>
        [JsonProperty]
        public bool ChangeAllowed { get; set; }

        /// <summary>
        /// Gets the time of the last change made by the Auphonic system (UTC).
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public DateTime ChangeTime { get; internal set; }

        /// <summary>
        /// Gets the channels of input audio.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public int? Channels { get; internal set; }

        /// <summary>
        /// Gets or sets a list of chapters.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<Chapter> Chapters { get; set; }

        /// <summary>
        /// Gets the production creation time (UTC).
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public DateTime CreationTime { get; internal set; }

        /// <summary>
        /// Gets or sets the time in seconds to cut from the end of an audio file.
        /// </summary>
        [JsonProperty]
        public decimal CutEnd { get; set; }

        /// <summary>
        /// Gets or sets the time in seconds to cut from the start of an audio file.
        /// </summary>
        [JsonProperty]
        public decimal CutStart { get; set; }

        /// <summary>
        /// Gets the URL to the Auphonic edit web page.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string EditPage { get; internal set; }

        /// <summary>
        /// Gets an error message, if an error occurred while processing,
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string ErrorMessage { get; internal set; }

        /// <summary>
        /// Gets the processing status, where an error occurred. (See <see cref="Status"/>.)
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string ErrorStatus { get; internal set; }

        /// <summary>
        /// Gets the input audio file format.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string Format { get; internal set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the production has a video.
        /// </summary>
        [JsonProperty]
        public bool HasVideo { get; set; }

        /// <summary>
        /// Gets or sets the image.
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the input file.
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string InputFile { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the production is multitrack.
        /// </summary>
        [JsonProperty]
        public bool IsMultitrack { get; set; }

        /// <summary>
        /// Gets the output audio length in seconds.
        /// </summary>
        /// <remarks>
        /// <strong>Important:</strong> if you have intro or outro files, they are included in this
        /// length.
        /// </remarks>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public decimal? Length { get; internal set; }

        /// <summary>
        /// Gets the output audio length in HH:MM:SS.mmm.
        /// </summary>
        /// <remarks>
        /// <strong>Important:</strong> if you have intro or outro files, they are included in this
        /// length.
        /// </remarks>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string LengthTimestring { get; internal set; }

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
        /// Gets or sets the preset UUID.
        /// </summary>
        [DefaultValue("")]
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore)]
        public string Preset { get; set; }

        /// <summary>
        /// Gets the input audio samplerate.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public int? Samplerate { get; internal set; }

        /// <summary>
        /// Gets or sets the service UUID for incoming file transfer (<strong>null</strong> for
        /// file upload or HTTP incoming).
        /// </summary>
        [JsonProperty]
        public string Service { get; set; }

        /// <summary>
        /// Gets or sets the speech recognition.
        /// </summary>
        [JsonProperty]
        public SpeechRecognition SpeechRecognition { get; set; }

        /// <summary>
        /// Gets or sets a value that indicates whether the production start is allowed.
        /// </summary>
        [JsonProperty]
        public bool StartAllowed { get; set; }

        /// <summary>
        /// Gets the audio processing statistics.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public Statistics Statistics { get; internal set; }

        /// <summary>
        /// Gets the production status.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public ProductionStatus Status { get; internal set; }

        /// <summary>
        /// Gets the URL to the Auphonic status web page.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string StatusPage { get; internal set; }

        /// <summary>
        /// Gets the production status string.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string StatusString { get; internal set; }

        /// <summary>
        /// Gets or sets the thumbnail.
        /// </summary>
        [JsonProperty]
        public string Thumbnail { get; set; }

        /// <summary>
        /// Gets used credits information.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public Credits UsedCredits { get; internal set; }

        /// <summary>
        /// Gets the production UUID.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string Uuid { get; internal set; }

        /// <summary>
        /// Gets a warning message, if a warning occurred while processing.
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string WarningMessage { get; internal set; }

        /// <summary>
        /// Gets the processing status, where a warning occurred. (See <see cref="Status"/>.)
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string WarningStatus { get; internal set; }

        /// <summary>
        /// Gets the URL to the image of the final waveform (as used by the audio player on the
        /// Auphonic status page).
        /// </summary>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string WaveformImage { get; internal set; }

        /// <summary>
        /// Gets or sets the webhook URL.
        /// </summary>
        /// <remarks>For details see <a href="https://auphonic.com/help/api/webhook.html">Webhooks</a>.</remarks>
        [JsonProperty]
        public string Webhook { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Production"/> class.
        /// </summary>
        public Production()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Production"/> class.
        /// </summary>
        /// <param name="presetUuid">The preset UUID.</param>
        /// <exception cref="ArgumentException"><em>presetUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>presetUuid</em> is <strong>null</strong>.</exception>
        public Production(string presetUuid)
        {
            Precondition.IsNotNullOrWhiteSpace(presetUuid, nameof(presetUuid));

            Preset = presetUuid;
        }
        #endregion
    }
}
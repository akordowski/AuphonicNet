using Newtonsoft.Json;
using System;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a chapter.
    /// </summary>
    public class Chapter
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets an (optional) image with visual information, e.g. slides or photos. The
        /// image will be shown in podcast players while listening to the chapter, or exported to
        /// video output files.
        /// </summary>
        [JsonProperty]
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the start of the chapter as timestring (in HH:MM:SS.mmm), relative to the
        /// main input file.
        /// </summary>
        /// <remarks>
        /// <strong>Important:</strong> if you have an additional intro or use
        /// <see cref="Production.CutStart"/>, its length is <strong>NOT included</strong> here.
        /// Enter chapter start time in HH:MM:SS.mmm format (examples: 00:02:35.500, 1:30, 3:25.5).
        /// </remarks>
        [JsonProperty]
        public string Start { get; set; }

        /// <summary>
        /// Gets the start of the chapter as timestring (in HH:MM:SS.mmm), relative to output
        /// files.
        /// </summary>
        /// <remarks>
        /// <strong>Important:</strong> if you have an additional intro or use
        /// <see cref="Production.CutStart"/>, its length <strong>is included</strong> here.
        /// </remarks>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public string StartOutput { get; internal set; }

        /// <summary>
        /// Gets the start of the chapter in seconds, relative to output files.
        /// </summary>
        /// <remarks>
        /// <strong>Important:</strong> if you have an additional intro or use
        /// <see cref="Production.CutStart"/>, its length <strong>is included</strong> here.
        /// </remarks>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public decimal StartOutputSec { get; internal set; }

        /// <summary>
        /// Gets the start of the current chapter in seconds, relative to the main input file.
        /// </summary>
        /// <remarks>
        /// <strong>Important:</strong> if you have an additional intro or use
        /// <see cref="Production.CutStart"/>, its length is <strong>NOT included</strong> here.
        /// </remarks>
        [JsonIgnoreSerialization]
        [JsonProperty]
        public decimal StartSec { get; internal set; }

        /// <summary>
        /// Gets or sets a (optional) title of the chapter. Audio players show chapter titles for
        /// quick navigation in audio files.
        /// </summary>
        [JsonProperty]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets an (optional) URL with further information about the chapter.
        /// </summary>
        [JsonProperty]
        public string Url { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Chapter"/> class.
        /// </summary>
        internal Chapter()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chapter"/> class.
        /// </summary>
        /// <param name="start">Start time of the chapter as timestring (in HH:MM:SS.mmm, e.g.
        /// 00:02:35.500, 1:30, 3:25.5).</param>
        /// <param name="title">Title of the chapter.</param>
        /// <exception cref="ArgumentException"><em>start</em> or <em>title</em> is empty or
        /// whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>start</em> or <em>title</em> is
        /// <strong>null</strong>.</exception>
        public Chapter(string start, string title)
            : this(start, title, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Chapter"/> class.
        /// </summary>
        /// <param name="start">Start time of the chapter as timestring (in HH:MM:SS.mmm, e.g.
        /// 00:02:35.500, 1:30, 3:25.5).</param>
        /// <param name="title">Title of the chapter.</param>
        /// <param name="url">URL with further information about the chapter.</param>
        /// <param name="image">Image with visual information about the chapter.</param>
        /// <exception cref="ArgumentException"><em>start</em> or <em>title</em> is empty or
        /// whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>start</em> or <em>title</em> is
        /// <strong>null</strong>.</exception>
        public Chapter(string start, string title, string url, string image)
        {
            Precondition.IsNotNullOrWhiteSpace(start, nameof(start));
            Precondition.IsNotNullOrWhiteSpace(title, nameof(title));

            Start = start;
            Title = title;
            Url = url;
            Image = image;
        }
        #endregion

        #region Public Override Methods
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return Title;
        }
        #endregion
    }
}
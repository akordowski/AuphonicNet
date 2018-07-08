using Newtonsoft.Json;
using System.Collections.Generic;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represent the metadata of a preset/production.
    /// </summary>
    public class Metadata
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the album name.
        /// </summary>
        [JsonProperty]
        public string Album { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the chapter marks are appended to the summary.
        /// </summary>
        /// <value><strong>true</strong> if the chapter marks are appended to the summary;
        /// otherwise, <strong>false</strong>. The default is <strong>false</strong>.</value>
        [JsonProperty]
        public bool AppendChapters { get; set; }

        /// <summary>
        /// Gets or sets the artist.
        /// </summary>
        [JsonProperty]
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets the genre.
        /// </summary>
        [JsonProperty]
        public string Genre { get; set; }

        /// <summary>
        /// Gets or sets the license.
        /// </summary>
        [JsonProperty]
        public string License { get; set; }

        /// <summary>
        /// Gets or sets the license URL.
        /// </summary>
        [JsonProperty]
        public string LicenseUrl { get; set; }

        /// <summary>
        /// Gets or sets the location.
        /// </summary>
        [JsonProperty]
        public Location Location { get; set; }

        /// <summary>
        /// Gets or sets the publisher.
        /// </summary>
        [JsonProperty]
        public string Publisher { get; set; }

        /// <summary>
        /// Gets or sets the subtitle.
        /// </summary>
        [JsonProperty]
        public string Subtitle { get; set; }

        /// <summary>
        /// Gets or sets the summary.
        /// </summary>
        [JsonProperty]
        public string Summary { get; set; }

        /// <summary>
        /// Gets or sets a list of tags.
        /// </summary>
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public List<string> Tags { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        [JsonProperty]
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the track number.
        /// </summary>
        [JsonProperty]
        public string Track { get; set; }

        /// <summary>
        /// Gets or sets a URL for further information.
        /// </summary>
        [JsonProperty]
        public string Url { get; set; }

        /// <summary>
        /// Gets or sets the year.
        /// </summary>
        [JsonProperty]
        public string Year { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Metadata"/> class.
        /// </summary>
        public Metadata()
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
            return Title;
        }
        #endregion
    }
}
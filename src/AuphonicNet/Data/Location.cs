using Newtonsoft.Json;
using System;
using System.Globalization;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a location.
    /// </summary>
    public class Location
    {
        #region Public Properties
        /// <summary>
        /// Gets the latitude.
        /// </summary>
        [JsonProperty]
        public string Latitude { get; internal set; }

        /// <summary>
        /// Gets the longitude.
        /// </summary>
        [JsonProperty]
        public string Longitude { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        internal Location()
            : this(0, 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Location"/> class.
        /// </summary>
        /// <param name="latitude">The latitude.</param>
        /// <param name="longitude">The longitude.</param>
        public Location(double? latitude, double? longitude)
        {
            IFormatProvider formatProvider = new CultureInfo("en-US");

            Latitude = latitude.GetValueOrDefault().ToString("0.000", formatProvider);
            Longitude = longitude.GetValueOrDefault().ToString("0.000", formatProvider);
        }
        #endregion

        #region Public Override Methods
        /// <summary>
        /// Returns a string that represents the current object.
        /// </summary>
        /// <returns>A string that represents the current object.</returns>
        public override string ToString()
        {
            return $"Latitude = {Latitude}; Longitude = {Longitude}";
        }
        #endregion
    }
}
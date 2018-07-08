using Newtonsoft.Json;

namespace AuphonicNet.OAuth
{
    /// <summary>
    /// Represents a OAuth token.
    /// </summary>
    public class OAuthToken
    {
        #region Public Properties
        /// <summary>
        /// Gets the access token.
        /// </summary>
        [JsonProperty]
        public string AccessToken { get; internal set; }

        /// <summary>
        /// Gets the token type.
        /// </summary>
        [JsonProperty]
        public string TokenType { get; internal set; }

        /// <summary>
        /// Gets the expires timespan in seconds.
        /// </summary>
        [JsonProperty]
        public int ExpiresIn { get; internal set; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        [JsonProperty("user_name")]
        public string Username { get; internal set; }

        /// <summary>
        /// Gets the token scope.
        /// </summary>
        [JsonProperty]
        public string Scope { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="OAuthToken"/> class.
        /// </summary>
        internal OAuthToken()
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
            return $"AccessToken = {AccessToken}; TokenType = {TokenType}";
        }
        #endregion
    }
}
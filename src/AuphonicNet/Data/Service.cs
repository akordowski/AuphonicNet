using Newtonsoft.Json;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a registered external services.
    /// </summary>
    public class Service
    {
        #region Public Properties
        /// <summary>
        /// Gets the base URL of the service.
        /// </summary>
        [JsonProperty]
        public string BaseUrl { get; internal set; }

        /// <summary>
        /// Gets the bucket name of the service.
        /// </summary>
        [JsonProperty]
        public string Bucket { get; internal set; }

        /// <summary>
        /// Gets the ACL of the service.
        /// </summary>
        [JsonProperty]
        public string CannedAcl { get; internal set; }

        /// <summary>
        /// Gets the name of the service.
        /// </summary>
        [JsonProperty]
        public string DisplayName { get; internal set; }

        /// <summary>
        /// Gets the email of the service.
        /// </summary>
        [JsonProperty]
        public string Email { get; internal set; }

        /// <summary>
        /// Gets the host of the service.
        /// </summary>
        [JsonProperty]
        public string Host { get; internal set; }

        /// <summary>
        /// Gets a value that indicates whether the service can be used as incoming file transfer.
        /// </summary>
        [JsonProperty]
        public bool Incoming { get; internal set; }

        /// <summary>
        /// Gets the key prefix of the service.
        /// </summary>
        [JsonProperty]
        public string KeyPrefix { get; internal set; }

        /// <summary>
        /// Gets the Libsyn service directory.
        /// </summary>
        [JsonProperty]
        public string LibsynDirectory { get; internal set; }

        /// <summary>
        /// Gets the Libsyn service slug.
        /// </summary>
        [JsonProperty]
        public string LibsynShowSlug { get; internal set; }

        /// <summary>
        /// Gets a value that indicates whether the service can be used as outgoing file transfer.
        /// </summary>
        [JsonProperty]
        public bool Outgoing { get; internal set; }

        /// <summary>
        /// Gets the path of the service.
        /// </summary>
        [JsonProperty]
        public string Path { get; internal set; }

        /// <summary>
        /// Gets the permissions of the service.
        /// </summary>
        [JsonProperty]
        public string Permissions { get; internal set; }

        /// <summary>
        /// Gets the port of the service.
        /// </summary>
        [JsonProperty]
        public string Port { get; internal set; }

        /// <summary>
        /// Gets the program keyword of the service.
        /// </summary>
        [JsonProperty]
        public string ProgramKeyword { get; internal set; }

        /// <summary>
        /// Gets the type of the service.
        /// </summary>
        [JsonProperty]
        public string Type { get; internal set; }

        /// <summary>
        /// Gets the URL of the service.
        /// </summary>
        [JsonProperty]
        public string Url { get; internal set; }

        /// <summary>
        /// Gets the service UUID.
        /// </summary>
        [JsonProperty]
        public string Uuid { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Service"/> class.
        /// </summary>
        internal Service()
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
using Newtonsoft.Json;
using System;

namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents the Auphonic account.
    /// </summary>
    public class Account
    {
        #region Public Properties
        /// <summary>
        /// Gets the credits.
        /// </summary>
        [JsonProperty]
        public decimal Credits { get; internal set; }

        /// <summary>
        /// Gets the user join date.
        /// </summary>
        [JsonProperty]
        public DateTime DateJoined { get; internal set; }

        /// <summary>
        /// Gets the user email.
        /// </summary>
        [JsonProperty]
        public string Email { get; internal set; }

        /// <summary>
        /// Gets a value that indicates whether the user recieve an email when a error occurs.
        /// </summary>
        [JsonProperty]
        public bool ErrorEmail { get; internal set; }

        /// <summary>
        /// Gets a value that indicates whether the user recieve an email when the low credits
        /// threshold is reached.
        /// </summary>
        [JsonProperty]
        public bool LowCreditsEmail { get; internal set; }

        /// <summary>
        /// Gets the low credits threshold.
        /// </summary>
        [JsonProperty]
        public decimal LowCreditsThreshold { get; internal set; }

        /// <summary>
        /// Gets a value that indicates whether the user recieve notification email.
        /// </summary>
        [JsonProperty]
        public bool NotificationEmail { get; internal set; }

        /// <summary>
        /// Gets a value of the onetime credits.
        /// </summary>
        [JsonProperty]
        public decimal OnetimeCredits { get; internal set; }

        /// <summary>
        /// Gets the credits recharge date.
        /// </summary>
        [JsonProperty]
        public DateTime RechargeDate { get; internal set; }

        /// <summary>
        /// Gets a value of the recharge recurring credits.
        /// </summary>
        [JsonProperty]
        public decimal RechargeRecurringCredits { get; internal set; }

        /// <summary>
        /// Gets a value of the recurring credits.
        /// </summary>
        [JsonProperty]
        public decimal RecurringCredits { get; internal set; }

        /// <summary>
        /// Gets the user ID.
        /// </summary>
        [JsonProperty]
        public string UserId { get; internal set; }

        /// <summary>
        /// Gets the username.
        /// </summary>
        [JsonProperty]
        public string Username { get; internal set; }

        /// <summary>
        /// Gets a value that indicates whether the user recieve warning email.
        /// </summary>
        [JsonProperty]
        public bool WarningEmail { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Account"/> class.
        /// </summary>
        internal Account()
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
            return Username;
        }
        #endregion
    }
}
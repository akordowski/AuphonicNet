namespace AuphonicNet.Data
{
    /// <summary>
    /// Provides a <see cref="ResponseError"/> class.
    /// </summary>
    public class ResponseError
    {
        #region Public Properties
        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        public string Error { get; set; }

        /// <summary>
        /// Gets or sets the error description.
        /// </summary>
        public string ErrorDescription { get; set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="ResponseError"/> class.
        /// </summary>
        public ResponseError()
        {
        }
        #endregion
    }
}
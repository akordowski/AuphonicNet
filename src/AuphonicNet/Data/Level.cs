namespace AuphonicNet.Data
{
    /// <summary>
    /// Represents a level.
    /// </summary>
    public class Level
    {
        #region Public Properties
        /// <summary>
        /// Gets the level unit.
        /// </summary>
        public string Unit { get; internal set; }

        /// <summary>
        /// Gets the level value.
        /// </summary>
        public decimal Value { get; internal set; }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Level"/> class.
        /// </summary>
        internal Level()
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
            return $"{Value} {Unit}".Trim();
        }
        #endregion
    }
}
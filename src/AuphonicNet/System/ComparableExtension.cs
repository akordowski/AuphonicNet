namespace System
{
    /// <summary>
    /// Contains <see cref="IComparable"/> extension methods.
    /// </summary>
    internal static class ComparableExtension
    {
        #region Public Static Methods
        /// <summary>
        /// Indicates whether the instance is between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The instance to test.</param>
        /// <param name="min">The minimum value to test.</param>
        /// <param name="max">The maximum value to test.</param>
        /// <returns><strong>true</strong> if the instance is between the minimum and maximum
        /// value; otherwise, <strong>false</strong>.</returns>
        /// <exception cref="ArgumentNullException"><em>value</em>, <em>min</em> or <em>max</em> is
        /// <strong>null</strong>.</exception>
        public static bool IsBetween(this IComparable value, IComparable min, IComparable max)
        {
            Precondition.IsNotNull(value, nameof(value));
            Precondition.IsNotNull(min, nameof(min));
            Precondition.IsNotNull(max, nameof(max));

            return value.IsGreaterOrEqual(min) && value.IsLessOrEqual(max);
        }

        /// <summary>
        /// Indicates whether the instance is equal to the reference value.
        /// </summary>
        /// <param name="value">The instance to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <returns><strong>true</strong> if the instance is equal to the reference value;
        /// otherwise, <strong>false</strong>.</returns>
        /// <exception cref="ArgumentNullException"><em>value</em> or <em>referenceValue</em> is
        /// <strong>null</strong>.</exception>
        public static bool IsEqual(this IComparable value, IComparable referenceValue)
        {
            Precondition.IsNotNull(value, nameof(value));
            Precondition.IsNotNull(referenceValue, nameof(referenceValue));

            return value.CompareTo(referenceValue) == 0;
        }

        /// <summary>
        /// Indicates whether the instance is greater or equal to the reference value.
        /// </summary>
        /// <param name="value">The instance to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <returns><strong>true</strong> if the instance is greater or equal to the reference
        /// value; otherwise, <strong>false</strong>.</returns>
        /// <exception cref="ArgumentNullException"><em>value</em> or <em>referenceValue</em> is
        /// <strong>null</strong>.</exception>
        public static bool IsGreaterOrEqual(this IComparable value, IComparable referenceValue)
        {
            Precondition.IsNotNull(value, nameof(value));
            Precondition.IsNotNull(referenceValue, nameof(referenceValue));

            return value.IsGreater(referenceValue) || value.IsEqual(referenceValue);
        }

        /// <summary>
        /// Indicates whether the instance is greater as the reference value.
        /// </summary>
        /// <param name="value">The instance to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <returns><strong>true</strong> if the instance is greater as the reference value;
        /// otherwise, <strong>false</strong>.</returns>
        /// <exception cref="ArgumentNullException"><em>value</em> or <em>referenceValue</em> is
        /// <strong>null</strong>.</exception>
        public static bool IsGreater(this IComparable value, IComparable referenceValue)
        {
            Precondition.IsNotNull(value, nameof(value));
            Precondition.IsNotNull(referenceValue, nameof(referenceValue));

            return value.CompareTo(referenceValue) > 0;
        }

        /// <summary>
        /// Indicates whether the instance is less or equal to the reference value.
        /// </summary>
        /// <param name="value">The instance to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <returns><strong>true</strong> if the instance is less or equal to the reference
        /// value; otherwise, <strong>false</strong>.</returns>
        /// <exception cref="ArgumentNullException"><em>value</em> or <em>referenceValue</em> is
        /// <strong>null</strong>.</exception>
        public static bool IsLessOrEqual(this IComparable value, IComparable referenceValue)
        {
            Precondition.IsNotNull(value, nameof(value));
            Precondition.IsNotNull(referenceValue, nameof(referenceValue));

            return value.IsLess(referenceValue) || value.IsEqual(referenceValue);
        }

        /// <summary>
        /// Indicates whether the instance is less as the reference value.
        /// </summary>
        /// <param name="value">The instance to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <returns><strong>true</strong> if the instance is less as the reference value;
        /// otherwise, <strong>false</strong>.</returns>
        /// <exception cref="ArgumentNullException"><em>value</em> or <em>referenceValue</em> is
        /// <strong>null</strong>.</exception>
        public static bool IsLess(this IComparable value, IComparable referenceValue)
        {
            Precondition.IsNotNull(value, nameof(value));
            Precondition.IsNotNull(referenceValue, nameof(referenceValue));

            return value.CompareTo(referenceValue) < 0;
        }
        #endregion
    }
}
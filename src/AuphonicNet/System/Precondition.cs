using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace System
{
    /// <summary>
    /// Provides static methods that help a constructor or method to verify correct arguments and
    /// state.
    /// </summary>
    internal static class Precondition
    {
        #region Public Static Methods
        public static void IsValid(Func<bool> predicate)
        {
            if (predicate())
            {
                throw new ArgumentException();
            }
        }

        public static void IsValid(Func<bool> predicate, string message)
        {
            if (predicate())
            {
                throw new ArgumentException(message);
            }
        }

        public static void IsValid(Func<bool> predicate, string message, string paramName)
        {
            if (predicate())
            {
                throw new ArgumentException(message, paramName);
            }
        }
        #endregion

        #region Public Static Methods - Object
        /// <summary>
        /// Checks whether the specified object is not <strong>null</strong>.
        /// </summary>
        /// <param name="obj">The object to test.</param>
        /// <exception cref="ArgumentNullException"><em>obj</em> is
        /// <strong>null</strong>.</exception>
        public static void IsNotNull(object obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException();
            }
        }

        /// <summary>
        /// Checks whether the specified object is not <strong>null</strong>.
        /// </summary>
        /// <param name="obj">The object to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentNullException"><em>obj</em> is
        /// <strong>null</strong>.</exception>
        public static void IsNotNull(object obj, string paramName)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName);
            }
        }

        /// <summary>
        /// Checks whether the specified object is not <strong>null</strong>.
        /// </summary>
        /// <param name="obj">The object to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentNullException"><em>obj</em> is
        /// <strong>null</strong>.</exception>
        public static void IsNotNull(object obj, string paramName, string message)
        {
            if (obj == null)
            {
                throw new ArgumentNullException(paramName, message);
            }
        }
        #endregion

        #region Public Static Methods - IEnumerable
        /// <summary>
        /// Checks whether the specified collection is not <strong>null</strong> or an empty
        /// collection.
        /// </summary>
        /// <param name="col">The collection to test.</param>
        /// <exception cref="ArgumentNullException"><em>col</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="ArgumentException"><em>col</em> is
        /// <strong>empty</strong>.</exception>
        public static void IsNotNullOrEmpty<T>(IEnumerable<T> col)
        {
            if (col == null)
            {
                throw new ArgumentNullException();
            }
            else if (!col.Any())
            {
                throw new ArgumentException();
            }
        }

        /// <summary>
        /// Checks whether the specified collection is not <strong>null</strong> or an empty
        /// collection.
        /// </summary>
        /// <param name="col">The collection to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentNullException"><em>col</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="ArgumentException"><em>col</em> is
        /// <strong>empty</strong>.</exception>
        public static void IsNotNullOrEmpty<T>(IEnumerable<T> col, string paramName)
        {
            if (col == null)
            {
                throw new ArgumentNullException(paramName);
            }
            else if (!col.Any())
            {
                string message = "Value cannot be an empty collection.";
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// Checks whether the specified collection is not <strong>null</strong> or an empty
        /// collection.
        /// </summary>
        /// <param name="col">The collection to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="messageArgNull">A message that describes the null error.</param>
        /// <param name="messageArgEmpty">A message that describes the empty error.</param>
        /// <exception cref="ArgumentNullException"><em>col</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="ArgumentException"><em>col</em> is
        /// <strong>empty</strong>.</exception>
        public static void IsNotNullOrEmpty<T>(IEnumerable<T> col, string paramName, string messageArgNull, string messageArgEmpty)
        {
            if (col == null)
            {
                throw new ArgumentNullException(paramName, messageArgNull);
            }
            else if (!col.Any())
            {
                throw new ArgumentException(paramName, messageArgEmpty);
            }
        }
        #endregion

        #region Public Static Methods - IComparable
        /// <summary>
        /// Checks whether the value is between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="min">The minimum value to test.</param>
        /// <param name="max">The maximum value to test.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsBetween(IComparable value, IComparable min, IComparable max)
        {
            if (!value.IsBetween(min, max))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Checks whether the value is between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="min">The minimum value to test.</param>
        /// <param name="max">The maximum value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsBetween(IComparable value, IComparable min, IComparable max, string paramName)
        {
            if (!value.IsBetween(min, max))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Checks whether the value is between a minimum and maximum value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="min">The minimum value to test.</param>
        /// <param name="max">The maximum value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsBetween(IComparable value, IComparable min, IComparable max, string paramName, string message)
        {
            if (!value.IsBetween(min, max))
            {
                throw new ArgumentOutOfRangeException(paramName, message);
            }
        }

        /// <summary>
        /// Checks whether the value is equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsEqual(IComparable value, IComparable referenceValue)
        {
            if (!value.IsEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Checks whether the value is equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsEqual(IComparable value, IComparable referenceValue, string paramName)
        {
            if (!value.IsEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Checks whether the value is equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsEqual(IComparable value, IComparable referenceValue, string paramName, string message)
        {
            if (!value.IsEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName, referenceValue, message);
            }
        }

        /// <summary>
        /// Checks whether the value is greater or equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsGreaterOrEqual(IComparable value, IComparable referenceValue)
        {
            if (!value.IsGreaterOrEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Checks whether the value is greater or equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsGreaterOrEqual(IComparable value, IComparable referenceValue, string paramName)
        {
            if (!value.IsGreaterOrEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Checks whether the value is greater or equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsGreaterOrEqual(IComparable value, IComparable referenceValue, string paramName, string message)
        {
            if (!value.IsGreaterOrEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName, referenceValue, message);
            }
        }

        /// <summary>
        /// Checks whether the value is greater as the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsGreater(IComparable value, IComparable referenceValue)
        {
            if (!value.IsGreater(referenceValue))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Checks whether the value is greater as the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsGreater(IComparable value, IComparable referenceValue, string paramName)
        {
            if (!value.IsGreater(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Checks whether the value is greater as the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsGreater(IComparable value, IComparable referenceValue, string paramName, string message)
        {
            if (!value.IsGreater(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName, referenceValue, message);
            }
        }

        /// <summary>
        /// Checks whether the value is less or equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsLessOrEqual(IComparable value, IComparable referenceValue)
        {
            if (!value.IsLessOrEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Checks whether the value is less or equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsLessOrEqual(IComparable value, IComparable referenceValue, string paramName)
        {
            if (!value.IsLessOrEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Checks whether the value is less or equal to the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsLessOrEqual(IComparable value, IComparable referenceValue, string paramName, string message)
        {
            if (!value.IsLessOrEqual(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName, referenceValue, message);
            }
        }

        /// <summary>
        /// Checks whether the value is less as the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsLess(IComparable value, IComparable referenceValue)
        {
            if (!value.IsLess(referenceValue))
            {
                throw new ArgumentOutOfRangeException();
            }
        }

        /// <summary>
        /// Checks whether the value is less as the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsLess(IComparable value, IComparable referenceValue, string paramName)
        {
            if (!value.IsLess(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName);
            }
        }

        /// <summary>
        /// Checks whether the value is less as the reference value.
        /// </summary>
        /// <param name="value">The value to test.</param>
        /// <param name="referenceValue">The reference value to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentOutOfRangeException"><em>value</em> is out of
        /// range.</exception>
        public static void IsLess(IComparable value, IComparable referenceValue, string paramName, string message)
        {
            if (!value.IsLess(referenceValue))
            {
                throw new ArgumentOutOfRangeException(paramName, referenceValue, message);
            }
        }
        #endregion

        #region Public Static Methods - File
        /// <summary>
        /// Checks whether the specified file exists.
        /// </summary>
        /// <param name="path">The file path to test.</param>
        /// <exception cref="ArgumentException"><em>path</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>path</em> is <strong>null</strong>.</exception>
        /// <exception cref="FileNotFoundException">File <em>path</em> don't exists.</exception>
        public static void FileExists(string path)
        {
            if (path == null)
            {
                throw new ArgumentNullException();
            }
            else if (string.IsNullOrWhiteSpace(path))
            {
                string msg = "Path cannot consists only of white-space characters.";
                throw new ArgumentException(msg);
            }
            else if (!File.Exists(path))
            {
                throw new FileNotFoundException(null, path);
            }
        }

        /// <summary>
        /// Checks whether the specified file exists.
        /// </summary>
        /// <param name="path">The file path to test.</param>
        /// <param name="message">A message that describes the error.</param>
        /// <exception cref="ArgumentException"><em>path</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>path</em> is <strong>null</strong>.</exception>
        /// <exception cref="FileNotFoundException">File <em>path</em> don't exists.</exception>
        public static void FileExists(string path, string message)
        {
            if (path == null)
            {
                throw new ArgumentNullException();
            }
            else if (string.IsNullOrWhiteSpace(path))
            {
                string msg = "Path cannot consists only of white-space characters.";
                throw new ArgumentException(msg);
            }
            else if (!File.Exists(path))
            {
                throw new FileNotFoundException(message, path);
            }
        }
        #endregion

        #region Public Static Methods - String
        /// <summary>
        /// Checks whether a specified string is not <strong>null</strong>, empty, or consists only
        /// of white-space characters.
        /// </summary>
        /// <param name="str">The string to test.</param>
        /// <exception cref="ArgumentNullException"><em>str</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="ArgumentException"><em>str</em> is
        /// <strong>whitespace</strong>.</exception>
        public static void IsNotNullOrWhiteSpace(string str)
        {
            if (str == null)
            {
                throw new ArgumentNullException();
            }
            else if (String.IsNullOrWhiteSpace(str))
            {
                string message = "Value cannot consists only of white-space characters.";
                throw new ArgumentException(message);
            }
        }

        /// <summary>
        /// Checks whether a specified string is not <strong>null</strong>, empty, or consists only
        /// of white-space characters.
        /// </summary>
        /// <param name="str">The string to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <exception cref="ArgumentNullException"><em>str</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="ArgumentException"><em>str</em> is
        /// <strong>whitespace</strong>.</exception>
        public static void IsNotNullOrWhiteSpace(string str, string paramName)
        {
            if (str == null)
            {
                throw new ArgumentNullException(paramName);
            }
            else if (String.IsNullOrWhiteSpace(str))
            {
                string message = "Value cannot consists only of white-space characters.";
                throw new ArgumentException(message, paramName);
            }
        }

        /// <summary>
        /// Checks whether a specified string is not <strong>null</strong>, empty, or consists only
        /// of white-space characters.
        /// </summary>
        /// <param name="str">The string to test.</param>
        /// <param name="paramName">The name of the parameter that caused the exception.</param>
        /// <param name="messageArgNull">A message that describes the null error.</param>
        /// <param name="messageArgWhiteSpace">A message that describes the whitespace
        /// error.</param>
        /// <exception cref="ArgumentNullException"><em>str</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="ArgumentException"><em>str</em> is
        /// <strong>whitespace</strong>.</exception>
        public static void IsNotNullOrWhiteSpace(string str, string paramName, string messageArgNull, string messageArgWhiteSpace)
        {
            if (str == null)
            {
                throw new ArgumentNullException(paramName, messageArgNull);
            }
            else if (String.IsNullOrWhiteSpace(str))
            {
                throw new ArgumentException(messageArgWhiteSpace, paramName);
            }
        }
        #endregion
    }
}
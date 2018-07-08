using AuphonicNet.Data;
using NUnit.Framework;
using System;
using System.Globalization;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class LevelTest
    {
        #region Tests
        [Test]
        public void Initialize_Constructor()
        {
            Level level = null;

            Assert.That(() => level = new Level(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(level.Value, Is.EqualTo(0), "Value");
                Assert.That(level.Unit, Is.Null, "Unit");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var level = new Level
            {
                Value = 1.23m,
                Unit = "dB"
            };

            IFormatProvider formatProvider = CultureInfo.CurrentCulture;

            Assert.That(level.ToString(), Is.EqualTo($"{level.Value.ToString(formatProvider)} {level.Unit}"));
        }
        #endregion
    }
}
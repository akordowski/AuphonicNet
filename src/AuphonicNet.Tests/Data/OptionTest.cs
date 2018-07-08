using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class OptionTest : TestBase<Option>
    {
        #region Constructor
        public OptionTest()
            : base("option.json")
        {
        }
        #endregion

        #region Tests
        [Test, Order(1)]
        public void Deserialize_Returns_Valid_Result()
        {
            Deserialize();

            Assert.Multiple(() =>
            {
                Assert.That(Item.DisplayName, Is.EqualTo("display_name"), "DisplayName");
                Assert.That(Item.Value, Is.EqualTo("value"), "Value");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"display_name\":"), Is.True, "display_name");
                Assert.That(json.Contains("\"value\":"), Is.True, "value");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Option option = null;

            Assert.That(() => option = new Option(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(option.DisplayName, Is.Null, "DisplayName");
                Assert.That(option.Value, Is.Null, "Value");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var option = new Option
            {
                DisplayName = "DisplayName",
                Value = "Value"
            };

            Assert.That(option.ToString(), Is.EqualTo($"{option.DisplayName} = {option.Value}"));
        }
        #endregion
    }
}
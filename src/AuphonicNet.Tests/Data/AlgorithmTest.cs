using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class AlgorithmTest : TestBase<Algorithm>
    {
        #region Constructor
        public AlgorithmTest()
            : base("algorithm.json")
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
                Assert.That(Item.BelongsTo, Is.EqualTo("belongs_to"), "BelongsTo");
                Assert.That(Item.DefaultValue, Is.EqualTo("default_value"), "DefaultValue");
                Assert.That(Item.Description, Is.EqualTo("description"), "Description");
                Assert.That(Item.DisplayName, Is.EqualTo("display_name"), "DisplayName");
                Assert.That(Item.Options.Count, Is.EqualTo(0), "Options");
                Assert.That(Item.Type, Is.EqualTo("type"), "Type");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"belongs_to\":"), Is.True, "belongs_to");
                Assert.That(json.Contains("\"default_value\":"), Is.True, "default_value");
                Assert.That(json.Contains("\"description\":"), Is.True, "description");
                Assert.That(json.Contains("\"display_name\":"), Is.True, "display_name");
                Assert.That(json.Contains("\"options\":"), Is.True, "options");
                Assert.That(json.Contains("\"type\":"), Is.True, "type");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Algorithm algorithmType = null;

            Assert.That(() => algorithmType = new Algorithm(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(algorithmType.BelongsTo, Is.Null, "BelongsTo");
                Assert.That(algorithmType.DefaultValue, Is.Null, "DefaultValue");
                Assert.That(algorithmType.Description, Is.Null, "Description");
                Assert.That(algorithmType.DisplayName, Is.Null, "DisplayName");
                Assert.That(algorithmType.Options, Is.Null, "Options");
                Assert.That(algorithmType.Type, Is.Null, "Type");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var algorithmType = new Algorithm
            {
                DisplayName = "DisplayName",
                DefaultValue = "DefaultValue"
            };

            Assert.That(algorithmType.ToString(), Is.EqualTo($"{algorithmType.DisplayName} = {algorithmType.DefaultValue}"));
        }
        #endregion
    }
}
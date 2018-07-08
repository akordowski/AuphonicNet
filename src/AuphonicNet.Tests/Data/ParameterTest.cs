using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class ParameterTest : TestBase<Parameter>
    {
        #region Constructor
        public ParameterTest()
            : base("parameter.json")
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
                Assert.That(Item.DefaultValue, Is.EqualTo("default_value"), "DefaultValue");
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
                Assert.That(json.Contains("\"default_value\":"), Is.True, "default_value");
                Assert.That(json.Contains("\"display_name\":"), Is.True, "display_name");
                Assert.That(json.Contains("\"options\":"), Is.True, "options");
                Assert.That(json.Contains("\"type\":"), Is.True, "type");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Parameter parameter = null;

            Assert.That(() => parameter = new Parameter(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(parameter.DefaultValue, Is.Null, "DefaultValue");
                Assert.That(parameter.DisplayName, Is.Null, "DisplayName");
                Assert.That(parameter.Options, Is.Null, "Options");
                Assert.That(parameter.Type, Is.Null, "Type");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var parameter = new Parameter
            {
                DisplayName = "DisplayName"
            };

            Assert.That(parameter.ToString(), Is.EqualTo(parameter.DisplayName));
        }
        #endregion
    }
}
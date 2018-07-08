using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class OutputFileTypeTest : TestBase<OutputFileType>
    {
        #region Constructor
        public OutputFileTypeTest()
            : base("output_file_type.json")
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
                Assert.That(Item.Bitrates.Count, Is.EqualTo(0), "Bitrates");
                Assert.That(Item.BitrateStrings.Count, Is.EqualTo(0), "BitrateStrings");
                Assert.That(Item.DefaultBitrate, Is.EqualTo("default_bitrate"), "DefaultBitrate");
                Assert.That(Item.DisplayName, Is.EqualTo("display_name"), "DisplayName");
                Assert.That(Item.Endings.Count, Is.EqualTo(0), "Endings");
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
                Assert.That(json.Contains("\"bitrates\":"), Is.True, "bitrates");
                Assert.That(json.Contains("\"bitrate_strings\":"), Is.True, "bitrate_strings");
                Assert.That(json.Contains("\"default_bitrate\":"), Is.True, "default_bitrate");
                Assert.That(json.Contains("\"display_name\":"), Is.True, "display_name");
                Assert.That(json.Contains("\"endings\":"), Is.True, "endings");
                Assert.That(json.Contains("\"type\":"), Is.True, "type");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            OutputFileType outputFileType = null;

            Assert.That(() => outputFileType = new OutputFileType(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(outputFileType.Bitrates, Is.Null, "Bitrates");
                Assert.That(outputFileType.BitrateStrings, Is.Null, "BitrateStrings");
                Assert.That(outputFileType.DefaultBitrate, Is.Null, "DefaultBitrate");
                Assert.That(outputFileType.DisplayName, Is.Null, "DisplayName");
                Assert.That(outputFileType.Endings, Is.Null, "Endings");
                Assert.That(outputFileType.Type, Is.Null, "Type");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var outputFileType = new OutputFileType
            {
                DisplayName = "DisplayName"
            };

            Assert.That(outputFileType.ToString(), Is.EqualTo(outputFileType.DisplayName));
        }
        #endregion
    }
}
using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class FormatTest : TestBase<Format>
    {
        #region Constructor
        public FormatTest()
            : base("format.json")
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
                Assert.That(Item.Bitrate, Is.EqualTo(320), "Bitrate");
                Assert.That(Item.Channels, Is.EqualTo(2), "Channels");
                Assert.That(Item.FileFormat, Is.EqualTo("mp3"), "FileFormat");
                Assert.That(Item.LengthSec, Is.EqualTo(123.45), "LengthSec");
                Assert.That(Item.SampleRate, Is.EqualTo(44100), "SampleRate");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"bitrate\":"), Is.True, "bitrate");
                Assert.That(json.Contains("\"channels\":"), Is.True, "channels");
                Assert.That(json.Contains("\"format\":"), Is.True, "format");
                Assert.That(json.Contains("\"length_sec\":"), Is.True, "length_sec");
                Assert.That(json.Contains("\"samplerate\":"), Is.True, "samplerate");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Format format = null;

            Assert.That(() => format = new Format(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(format.Bitrate, Is.EqualTo(0), "Bitrate");
                Assert.That(format.Channels, Is.EqualTo(0), "Channels");
                Assert.That(format.FileFormat, Is.Null, "FileFormat");
                Assert.That(format.LengthSec, Is.EqualTo(0), "LengthSec");
                Assert.That(format.SampleRate, Is.EqualTo(0), "SampleRate");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var format = new Format
            {
                FileFormat = "FileFormat"
            };

            Assert.That(format.ToString(), Is.EqualTo(format.FileFormat));
        }
        #endregion
    }
}
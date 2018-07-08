using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class MultiInputFileTest : TestBase<MultiInputFile>
    {
        #region Constructor
        public MultiInputFileTest()
            : base("multi_input_file.json")
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
                Assert.That(Item.Algorithms, Is.Not.Null, "Algorithms");
                Assert.That(Item.Id, Is.EqualTo("id"), "Id");
                Assert.That(Item.InputBitrate, Is.EqualTo(320), "InputBitrate");
                Assert.That(Item.InputChannels, Is.EqualTo(2), "InputChannels");
                Assert.That(Item.InputFile, Is.EqualTo("input file"), "InputFile");
                Assert.That(Item.InputFiletype, Is.EqualTo("input filetype"), "InputFiletype");
                Assert.That(Item.InputLength, Is.EqualTo(123), "InputLength");
                Assert.That(Item.InputSamplerate, Is.EqualTo(44100), "InputSamplerate");
                Assert.That(Item.Offset, Is.EqualTo(1.23), "Offset");
                Assert.That(Item.Service, Is.EqualTo("service"), "Service");
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
                Assert.That(json.Contains("\"algorithms\":"), Is.True, "algorithms");
                Assert.That(json.Contains("\"id\":"), Is.True, "id");
                Assert.That(json.Contains("\"input_bitrate\":"), Is.False, "input_bitrate");
                Assert.That(json.Contains("\"input_channels\":"), Is.False, "input_channels");
                Assert.That(json.Contains("\"input_file\":"), Is.True, "input_file");
                Assert.That(json.Contains("\"input_filetype\":"), Is.False, "input_filetype");
                Assert.That(json.Contains("\"input_length\":"), Is.False, "input_length");
                Assert.That(json.Contains("\"input_samplerate\":"), Is.False, "input_samplerate");
                Assert.That(json.Contains("\"offset\":"), Is.True, "offset");
                Assert.That(json.Contains("\"service\":"), Is.True, "service");
                Assert.That(json.Contains("\"type\":"), Is.True, "type");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            MultiInputFile multiInputFile = null;

            Assert.That(() => multiInputFile = new MultiInputFile(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(multiInputFile.Algorithms, Is.Null, "Algorithms");
                Assert.That(multiInputFile.Id, Is.Null, "Id");
                Assert.That(multiInputFile.InputBitrate, Is.EqualTo(0), "InputBitrate");
                Assert.That(multiInputFile.InputChannels, Is.EqualTo(0), "InputChannels");
                Assert.That(multiInputFile.InputFile, Is.Null, "InputFile");
                Assert.That(multiInputFile.InputFiletype, Is.Null, "InputFiletype");
                Assert.That(multiInputFile.InputLength, Is.EqualTo(0), "InputLength");
                Assert.That(multiInputFile.InputSamplerate, Is.EqualTo(0), "InputSamplerate");
                Assert.That(multiInputFile.Offset, Is.EqualTo(0), "Offset");
                Assert.That(multiInputFile.Service, Is.Null, "Service");
                Assert.That(multiInputFile.Type, Is.Null, "Type");
            });
        }
        #endregion
    }
}
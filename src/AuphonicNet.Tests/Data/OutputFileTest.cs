using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class OutputFileTest : TestBase<OutputFile>
    {
        #region Constructor
        public OutputFileTest()
            : base("output_file.json")
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
                Assert.That(Item.Checksum, Is.EqualTo("checksum"), "Checksum");
                Assert.That(Item.DownloadUrl, Is.EqualTo("download url"), "DownloadUrl");
                Assert.That(Item.Ending, Is.EqualTo("ending"), "Ending");
                Assert.That(Item.Filename, Is.EqualTo("filename"), "Filename");
                Assert.That(Item.Format, Is.EqualTo("format"), "Format");
                Assert.That(Item.MonoMixdown, Is.True, "MonoMixdown");
                Assert.That(Item.OutgoingServices.Count, Is.EqualTo(0), "OutgoingServices");
                Assert.That(Item.Size, Is.EqualTo(1024), "Size");
                Assert.That(Item.SizeString, Is.EqualTo("1024 kB"), "SizeString");
                Assert.That(Item.SplitOnChapters, Is.True, "SplitOnChapters");
                Assert.That(Item.Suffix, Is.EqualTo("suffix"), "Suffix");
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
                Assert.That(json.Contains("\"checksum\":"), Is.False, "checksum");
                Assert.That(json.Contains("\"download_url\":"), Is.False, "download_url");
                Assert.That(json.Contains("\"ending\":"), Is.True, "ending");
                Assert.That(json.Contains("\"filename\":"), Is.True, "filename");
                Assert.That(json.Contains("\"format\":"), Is.True, "format");
                Assert.That(json.Contains("\"mono_mixdown\":"), Is.True, "mono_mixdown");
                Assert.That(json.Contains("\"outgoing_services\":"), Is.True, "outgoing_services");
                Assert.That(json.Contains("\"size\":"), Is.False, "size");
                Assert.That(json.Contains("\"size_string\":"), Is.False, "size_string");
                Assert.That(json.Contains("\"split_on_chapters\":"), Is.True, "split_on_chapters");
                Assert.That(json.Contains("\"suffix\":"), Is.True, "suffix");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            OutputFile outputFile = null;

            Assert.That(() => outputFile = new OutputFile(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(outputFile.Bitrate, Is.EqualTo(0), "Bitrate");
                Assert.That(outputFile.Checksum, Is.Null, "Checksum");
                Assert.That(outputFile.DownloadUrl, Is.Null, "DownloadUrl");
                Assert.That(outputFile.Ending, Is.Null, "Ending");
                Assert.That(outputFile.Filename, Is.Null, "Filename");
                Assert.That(outputFile.Format, Is.Null, "Format");
                Assert.That(outputFile.MonoMixdown, Is.False, "MonoMixdown");
                Assert.That(outputFile.OutgoingServices, Is.Null, "OutgoingServices");
                Assert.That(outputFile.Size, Is.EqualTo(0), "Size");
                Assert.That(outputFile.SizeString, Is.Null, "SizeString");
                Assert.That(outputFile.SplitOnChapters, Is.False, "SplitOnChapters");
                Assert.That(outputFile.Suffix, Is.Null, "Suffix");
            });
        }
        #endregion
    }
}
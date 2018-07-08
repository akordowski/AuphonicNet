using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class MetadataTest : TestBase<Metadata>
    {
        #region Constructor
        public MetadataTest()
            : base("metadata.json")
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
                Assert.That(Item.Album, Is.EqualTo("album"), "Album");
                Assert.That(Item.AppendChapters, Is.True, "AppendChapters");
                Assert.That(Item.Artist, Is.EqualTo("artist"), "Artist");
                Assert.That(Item.Genre, Is.EqualTo("genre"), "Genre");
                Assert.That(Item.License, Is.EqualTo("license"), "License");
                Assert.That(Item.LicenseUrl, Is.EqualTo("license url"), "LicenseUrl");
                Assert.That(Item.Location, Is.Not.Null, "Location");
                Assert.That(Item.Publisher, Is.EqualTo("publisher"), "Publisher");
                Assert.That(Item.Subtitle, Is.EqualTo("subtitle"), "Subtitle");
                Assert.That(Item.Summary, Is.EqualTo("summary"), "Summary");
                Assert.That(Item.Tags.Count, Is.EqualTo(0), "Tags");
                Assert.That(Item.Title, Is.EqualTo("title"), "Title");
                Assert.That(Item.Track, Is.EqualTo("track"), "Track");
                Assert.That(Item.Url, Is.EqualTo("url"), "Url");
                Assert.That(Item.Year, Is.EqualTo("year"), "Year");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"album\":"), Is.True, "album");
                Assert.That(json.Contains("\"append_chapters\":"), Is.True, "append_chapters");
                Assert.That(json.Contains("\"artist\":"), Is.True, "artist");
                Assert.That(json.Contains("\"genre\":"), Is.True, "genre");
                Assert.That(json.Contains("\"license\":"), Is.True, "license");
                Assert.That(json.Contains("\"license_url\":"), Is.True, "license_url");
                Assert.That(json.Contains("\"location\":"), Is.True, "location");
                Assert.That(json.Contains("\"publisher\":"), Is.True, "publisher");
                Assert.That(json.Contains("\"subtitle\":"), Is.True, "subtitle");
                Assert.That(json.Contains("\"summary\":"), Is.True, "summary");
                Assert.That(json.Contains("\"tags\":"), Is.True, "tags");
                Assert.That(json.Contains("\"title\":"), Is.True, "title");
                Assert.That(json.Contains("\"track\":"), Is.True, "track");
                Assert.That(json.Contains("\"url\":"), Is.True, "url");
                Assert.That(json.Contains("\"year\":"), Is.True, "year");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Metadata metadata = null;

            Assert.That(() => metadata = new Metadata(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(metadata.Album, Is.Null, "Album");
                Assert.That(metadata.AppendChapters, Is.False, "AppendChapters");
                Assert.That(metadata.Artist, Is.Null, "Artist");
                Assert.That(metadata.Genre, Is.Null, "Genre");
                Assert.That(metadata.License, Is.Null, "License");
                Assert.That(metadata.LicenseUrl, Is.Null, "LicenseUrl");
                Assert.That(metadata.Location, Is.Null, "Location");
                Assert.That(metadata.Publisher, Is.Null, "Publisher");
                Assert.That(metadata.Subtitle, Is.Null, "Subtitle");
                Assert.That(metadata.Summary, Is.Null, "Summary");
                Assert.That(metadata.Tags, Is.Null, "Tags");
                Assert.That(metadata.Title, Is.Null, "Title");
                Assert.That(metadata.Track, Is.Null, "Track");
                Assert.That(metadata.Url, Is.Null, "Url");
                Assert.That(metadata.Year, Is.Null, "Year");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var metadata = new Metadata
            {
                Title = "Title"
            };

            Assert.That(metadata.ToString(), Is.EqualTo(metadata.Title));
        }
        #endregion
    }
}
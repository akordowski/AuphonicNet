using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class ChapterTest : TestBase<Chapter>
    {
        #region Constructor
        public ChapterTest()
            : base("chapter.json")
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
                Assert.That(Item.Image, Is.EqualTo("image"), "Image");
                Assert.That(Item.Start, Is.EqualTo("00:00:00"), "Start");
                Assert.That(Item.StartOutput, Is.EqualTo("00:00:00"), "StartOutput");
                Assert.That(Item.StartOutputSec, Is.EqualTo(0), "StartOutputSec");
                Assert.That(Item.StartSec, Is.EqualTo(0), "StartSec");
                Assert.That(Item.Title, Is.EqualTo("title"), "Title");
                Assert.That(Item.Url, Is.EqualTo("url"), "Url");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"start\":"), Is.True, "start");
                Assert.That(json.Contains("\"title\":"), Is.True, "title");
                Assert.That(json.Contains("\"url\":"), Is.True, "url");
                Assert.That(json.Contains("\"image\":"), Is.True, "image");
                Assert.That(json.Contains("\"start_output\":"), Is.False, "start_output");
                Assert.That(json.Contains("\"start_output_sec\":"), Is.False, "start_output_sec");
                Assert.That(json.Contains("\"start_sec\":"), Is.False, "start_sec");
            });
        }

        [Test]
        public void Initialize_Constructor_1()
        {
            Chapter chapter = null;

            Assert.That(() => chapter = new Chapter(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(chapter.Image, Is.Null, "Image");
                Assert.That(chapter.Start, Is.Null, "Start");
                Assert.That(chapter.StartOutput, Is.Null, "StartOutput");
                Assert.That(chapter.StartOutputSec, Is.EqualTo(0), "StartOutputSec");
                Assert.That(chapter.StartSec, Is.EqualTo(0), "StartSec");
                Assert.That(chapter.Title, Is.Null, "Title");
                Assert.That(chapter.Url, Is.Null, "Url");
            });
        }

        [Test]
        public void Initialize_Constructor_2(
            [Values(null, "", "  ", "00:00:00")] string start,
            [Values(null, "", "  ", "title")] string title)
        {
            Type expectedException = null;
            string expectedParamName = null;

            if (String.IsNullOrWhiteSpace(start))
            {
                expectedException = start == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(start);
            }
            else if (String.IsNullOrWhiteSpace(title))
            {
                expectedException = title == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(title);
            }

            if (expectedException == null)
            {
                Chapter chapter = null;

                Assert.That(() => chapter = new Chapter(start, title), Throws.Nothing);
                Assert.Multiple(() =>
                {
                    Assert.That(chapter.Image, Is.Null, "Image");
                    Assert.That(chapter.Start, Is.EqualTo("00:00:00"), "Start");
                    Assert.That(chapter.StartOutput, Is.Null, "StartOutput");
                    Assert.That(chapter.StartOutputSec, Is.EqualTo(0), "StartOutputSec");
                    Assert.That(chapter.StartSec, Is.EqualTo(0), "StartSec");
                    Assert.That(chapter.Title, Is.EqualTo("title"), "Title");
                    Assert.That(chapter.Url, Is.Null, "Url");
                });
            }
            else
            {
                Assert.That(() => new Chapter(start, title), Throws
                    .Exception.TypeOf(expectedException)
                    .With.Property("ParamName").EqualTo(expectedParamName));
            }
        }

        [Test]
        public void Initialize_Constructor_3(
            [Values(null, "", "  ", "00:00:00")] string start,
            [Values(null, "", "  ", "title")] string title,
            [Values(null, "", "  ", "url")] string url,
            [Values(null, "", "  ", "image")] string image)
        {
            Type expectedException = null;
            string expectedParamName = null;

            if (String.IsNullOrWhiteSpace(start))
            {
                expectedException = start == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(start);
            }
            else if (String.IsNullOrWhiteSpace(title))
            {
                expectedException = title == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(title);
            }

            if (expectedException == null)
            {
                Chapter chapter = null;

                Assert.That(() => chapter = new Chapter(start, title, url, image), Throws.Nothing);
                Assert.Multiple(() =>
                {
                    Assert.That(chapter.Image, Is.EqualTo(image), "Image");
                    Assert.That(chapter.Start, Is.EqualTo(start), "Start");
                    Assert.That(chapter.StartOutput, Is.Null, "StartOutput");
                    Assert.That(chapter.StartOutputSec, Is.EqualTo(0), "StartOutputSec");
                    Assert.That(chapter.StartSec, Is.EqualTo(0), "StartSec");
                    Assert.That(chapter.Title, Is.EqualTo(title), "Title");
                    Assert.That(chapter.Url, Is.EqualTo(url), "Url");
                });
            }
            else
            {
                Assert.That(() => new Chapter(start, title, url, image), Throws
                    .Exception.TypeOf(expectedException)
                    .With.Property("ParamName").EqualTo(expectedParamName));
            }
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var chapter = new Chapter
            {
                Title = "Title"
            };

            Assert.That(chapter.ToString(), Is.EqualTo(chapter.Title));
        }
        #endregion
    }
}
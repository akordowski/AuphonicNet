using AuphonicNet.Data;
using NUnit.Framework;
using System;
using System.Collections;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class SpeechRecognitionTest : TestBase<SpeechRecognition>
    {
        #region Constructor
        public SpeechRecognitionTest()
            : base("speech_recognition.json")
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
                Assert.That(Item.Keywords.Length, Is.EqualTo(1), "Keywords");
                Assert.That(Item.Language, Is.EqualTo("en-US"), "Language");
                Assert.That(Item.Type, Is.EqualTo("type"), "Type");
                Assert.That(Item.Uuid, Is.EqualTo("uuid"), "Uuid");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"keywords\":"), Is.True, "keywords");
                Assert.That(json.Contains("\"language\":"), Is.True, "language");
                Assert.That(json.Contains("\"type\":"), Is.True, "type");
                Assert.That(json.Contains("\"uuid\":"), Is.True, "uuid");
            });
        }

        [Test]
        public void Initialize_Constructor_1()
        {
            SpeechRecognition speechRecognition = null;

            Assert.That(() => speechRecognition = new SpeechRecognition(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(speechRecognition.Keywords, Is.Null, "Keywords");
                Assert.That(speechRecognition.Language, Is.Null, "Language");
                Assert.That(speechRecognition.Type, Is.Null, "Type");
                Assert.That(speechRecognition.Uuid, Is.Null, "Uuid");
            });
        }

        [Test]
        public void Initialize_Constructor_2(
            [Values(null, "", "  ", "00:00:00")] string uuid,
            [Values(null, "", "  ", "en-US")] string language,
            [ValueSource("KeywordValues")] string[] keywords)
        {
            Type expectedException = null;
            string expectedParamName = null;

            if (String.IsNullOrWhiteSpace(uuid))
            {
                expectedException = uuid == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(uuid);
            }
            else if (String.IsNullOrWhiteSpace(language))
            {
                expectedException = language == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(language);
            }
            else if (keywords == null || keywords.Length == 0)
            {
                expectedException = keywords == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(keywords);
            }

            if (expectedException == null)
            {
                SpeechRecognition speechRecognition = null;

                Assert.That(() => speechRecognition = new SpeechRecognition(uuid, language, keywords), Throws.Nothing);
                Assert.Multiple(() =>
                {
                    Assert.That(speechRecognition.Keywords.Length, Is.EqualTo(keywords.Length), "Keywords");
                    Assert.That(speechRecognition.Language, Is.EqualTo(language), "Language");
                    Assert.That(speechRecognition.Type, Is.Null, "Type");
                    Assert.That(speechRecognition.Uuid, Is.EqualTo(uuid), "Uuid");
                });
            }
            else
            {
                Assert.That(() => new SpeechRecognition(uuid, language, keywords), Throws
                    .Exception.TypeOf(expectedException)
                    .With.Property("ParamName").EqualTo(expectedParamName));
            }
        }
        #endregion

        #region Value Source
        public static IEnumerable KeywordValues()
        {
            yield return null;
            yield return new string[] { };
            yield return new string[] { "keyword" };
        }
        #endregion
    }
}
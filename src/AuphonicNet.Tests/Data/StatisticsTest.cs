using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class StatisticsTest : TestBase<Statistics>
    {
        #region Constructor
        public StatisticsTest()
            : base("statistics.json")
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
                Assert.That(Item.Format, Is.Not.Null, "Format");
                Assert.That(Item.Levels, Is.Not.Null, "Levels");
                Assert.That(Item.MusicSpeech.Count, Is.EqualTo(0), "MusicSpeech");
                Assert.That(Item.NoiseHumReduction.Count, Is.EqualTo(0), "NoiseHumReduction");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"format\":"), Is.True, "format");
                Assert.That(json.Contains("\"levels\":"), Is.True, "levels");
                Assert.That(json.Contains("\"music_speech\":"), Is.True, "music_speech");
                Assert.That(json.Contains("\"noise_hum_reduction\":"), Is.True, "noise_hum_reduction");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Statistics statistics = null;

            Assert.That(() => statistics = new Statistics(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(statistics.Format, Is.Null, "Format");
                Assert.That(statistics.Levels, Is.Null, "Levels");
                Assert.That(statistics.MusicSpeech, Is.Null, "MusicSpeech");
                Assert.That(statistics.NoiseHumReduction, Is.Null, "NoiseHumReduction");
            });
        }
        #endregion
    }
}
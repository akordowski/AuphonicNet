using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class LevelStatisticsTest : TestBase<LevelStatistics>
    {
        #region Constructor
        public LevelStatisticsTest()
            : base("level_statistics.json")
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
                Assert.That(Item.GainMax, Is.Not.Null, "GainMax");
                Assert.That(Item.GainMean, Is.Not.Null, "GainMean");
                Assert.That(Item.GainMin, Is.Not.Null, "GainMin");
                Assert.That(Item.Loudness, Is.Not.Null, "Loudness");
                Assert.That(Item.Lra, Is.Not.Null, "Lra");
                Assert.That(Item.MaxMomentary, Is.Not.Null, "MaxMomentary");
                Assert.That(Item.MaxShortterm, Is.Not.Null, "MaxShortterm");
                Assert.That(Item.NoiseLevel, Is.Not.Null, "NoiseLevel");
                Assert.That(Item.Peak, Is.Not.Null, "Peak");
                Assert.That(Item.SignalLevel, Is.Not.Null, "SignalLevel");
                Assert.That(Item.Snr, Is.Not.Null, "Snr");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"gain_max\":"), Is.True, "gain_max");
                Assert.That(json.Contains("\"gain_mean\":"), Is.True, "gain_mean");
                Assert.That(json.Contains("\"gain_min\":"), Is.True, "gain_min");
                Assert.That(json.Contains("\"loudness\":"), Is.True, "loudness");
                Assert.That(json.Contains("\"lra\":"), Is.True, "lra");
                Assert.That(json.Contains("\"max_momentary\":"), Is.True, "max_momentary");
                Assert.That(json.Contains("\"max_shortterm\":"), Is.True, "max_shortterm");
                Assert.That(json.Contains("\"noise_level\":"), Is.True, "noise_level");
                Assert.That(json.Contains("\"peak\":"), Is.True, "peak");
                Assert.That(json.Contains("\"signal_level\":"), Is.True, "signal_level");
                Assert.That(json.Contains("\"snr\":"), Is.True, "snr");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            LevelStatistics levelStatistic = null;

            Assert.That(() => levelStatistic = new LevelStatistics(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(levelStatistic.GainMax, Is.Null, "GainMax");
                Assert.That(levelStatistic.GainMean, Is.Null, "GainMean");
                Assert.That(levelStatistic.GainMin, Is.Null, "GainMin");
                Assert.That(levelStatistic.Loudness, Is.Null, "Loudness");
                Assert.That(levelStatistic.Lra, Is.Null, "Lra");
                Assert.That(levelStatistic.MaxMomentary, Is.Null, "MaxMomentary");
                Assert.That(levelStatistic.MaxShortterm, Is.Null, "MaxShortterm");
                Assert.That(levelStatistic.NoiseLevel, Is.Null, "NoiseLevel");
                Assert.That(levelStatistic.Peak, Is.Null, "Peak");
                Assert.That(levelStatistic.SignalLevel, Is.Null, "SignalLevel");
                Assert.That(levelStatistic.Snr, Is.Null, "Snr");
            });
        }
        #endregion
    }
}
using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class NoiseHumReductionTest : TestBase<NoiseHumReduction>
    {
        #region Constructor
        public NoiseHumReductionTest()
            : base("noise_hum_reduction.json")
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
                Assert.That(Item.Dehum, Is.Null, "Dehum");
                Assert.That(Item.Denoise, Is.Null, "Denoise");
                Assert.That(Item.Start, Is.EqualTo("start"), "Start");
                Assert.That(Item.StartSec, Is.EqualTo(0), "StartSec");
                Assert.That(Item.Stop, Is.EqualTo("stop"), "Stop");
                Assert.That(Item.StopSec, Is.EqualTo(0), "StopSec");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"dehum\":"), Is.True, "dehum");
                Assert.That(json.Contains("\"denoise\":"), Is.True, "denoise");
                Assert.That(json.Contains("\"start\":"), Is.True, "start");
                Assert.That(json.Contains("\"start_sec\":"), Is.True, "start_sec");
                Assert.That(json.Contains("\"stop\":"), Is.True, "stop");
                Assert.That(json.Contains("\"stop_sec\":"), Is.True, "stop_sec");
            });
        }
        [Test]
        public void Initialize_Constructor()
        {
            NoiseHumReduction noiseHumReduction = null;

            Assert.That(() => noiseHumReduction = new NoiseHumReduction(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(noiseHumReduction.Dehum, Is.Null, "Dehum");
                Assert.That(noiseHumReduction.Denoise, Is.Null, "Denoise");
                Assert.That(noiseHumReduction.Start, Is.Null, "Start");
                Assert.That(noiseHumReduction.Start, Is.Null, "StartSec");
                Assert.That(noiseHumReduction.Start, Is.Null, "Stop");
                Assert.That(noiseHumReduction.Start, Is.Null, "StopSec");
            });
        }
        #endregion
    }
}
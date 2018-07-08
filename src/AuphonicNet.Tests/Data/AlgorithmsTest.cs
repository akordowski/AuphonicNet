using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class AlgorithmsTest : TestBase<Algorithms>
    {
        #region Constructor
        public AlgorithmsTest()
            : base("algorithms.json")
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
                Assert.That(Item.Denoise, Is.True, "Denoise");
                Assert.That(Item.DenoiseAmount, Is.EqualTo(12), "DenoiseAmount");
                Assert.That(Item.HipFilter, Is.True, "HipFilter");
                Assert.That(Item.Leveler, Is.True, "Leveler");
                Assert.That(Item.LoudnessTarget, Is.EqualTo(34), "LoudnessTarget");
                Assert.That(Item.NormLoudness, Is.True, "NormLoudness");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"denoise\":"), Is.True, "denoise");
                Assert.That(json.Contains("\"denoiseamount\":"), Is.True, "denoiseamount");
                Assert.That(json.Contains("\"hipfilter\":"), Is.True, "hipfilter");
                Assert.That(json.Contains("\"leveler\":"), Is.True, "leveler");
                Assert.That(json.Contains("\"loudnesstarget\":"), Is.True, "loudnesstarget");
                Assert.That(json.Contains("\"normloudness\":"), Is.True, "normloudness");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Algorithms algorithms = null;

            Assert.That(() => algorithms = new Algorithms(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(algorithms.Denoise, Is.False, "Denoise");
                Assert.That(algorithms.DenoiseAmount, Is.EqualTo(0), "DenoiseAmount");
                Assert.That(algorithms.HipFilter, Is.False, "HipFilter");
                Assert.That(algorithms.Leveler, Is.False, "Leveler");
                Assert.That(algorithms.LoudnessTarget, Is.EqualTo(0), "LoudnessTarget");
                Assert.That(algorithms.NormLoudness, Is.False, "NormLoudness");
            });
        }
        #endregion
    }
}
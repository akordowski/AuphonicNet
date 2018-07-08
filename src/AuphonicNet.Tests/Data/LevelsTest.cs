using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class LevelsTest : TestBase<Levels>
    {
        #region Constructor
        public LevelsTest()
            : base("levels.json")
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
                Assert.That(Item.Input, Is.Not.Null, "Input");
                Assert.That(Item.Output, Is.Not.Null, "Output");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"input\":"), Is.True, "input");
                Assert.That(json.Contains("\"output\":"), Is.True, "output");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Levels levels = null;

            Assert.That(() => levels = new Levels(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(levels.Input, Is.Null, "Input");
                Assert.That(levels.Output, Is.Null, "Output");
            });
        }
        #endregion
    }
}
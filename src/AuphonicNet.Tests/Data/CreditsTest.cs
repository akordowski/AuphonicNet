using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class CreditsTest : TestBase<Credits>
    {
        #region Constructor
        public CreditsTest()
            : base("credits.json")
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
                Assert.That(Item.Combined, Is.EqualTo(1.23), "Combined");
                Assert.That(Item.Onetime, Is.EqualTo(2.34), "Onetime");
                Assert.That(Item.Recurring, Is.EqualTo(3.45), "Recurring");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"combined\":"), Is.True, "combined");
                Assert.That(json.Contains("\"onetime\":"), Is.True, "onetime");
                Assert.That(json.Contains("\"recurring\":"), Is.True, "recurring");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Credits credits = null;

            Assert.That(() => credits = new Credits(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(credits.Combined, Is.EqualTo(0), "Combined");
                Assert.That(credits.Onetime, Is.EqualTo(0), "Onetime");
                Assert.That(credits.Recurring, Is.EqualTo(0), "Recurring");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var credits = new Credits
            {
                Combined = 1.23m,
                Onetime = 4.56m,
                Recurring = 7.89m
            };

            Assert.That(credits.ToString(), Is.EqualTo($"Combined = {credits.Combined}; Onetime = {credits.Onetime}; Recurring = {credits.Recurring}"));
        }
        #endregion
    }
}
using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class LocationTest : TestBase<Location>
    {
        #region Constructor
        public LocationTest()
            : base("location.json")
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
                Assert.That(Item.Latitude, Is.EqualTo("1.234"), "Latitude");
                Assert.That(Item.Longitude, Is.EqualTo("5.678"), "Longitude");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"latitude\":"), Is.True, "latitude");
                Assert.That(json.Contains("\"longitude\":"), Is.True, "longitude");
            });
        }

        [Test]
        public void Initialize_Constructor_1()
        {
            Location location = null;

            Assert.That(() => location = new Location(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(location.Latitude, Is.EqualTo("0.000"), "Latitude");
                Assert.That(location.Longitude, Is.EqualTo("0.000"), "Longitude");
            });
        }

        [Test]
        public void Initialize_Constructor_2()
        {
            Location location = null;

            Assert.That(() => location = new Location(1.234, 5.678), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(location.Latitude, Is.EqualTo("1.234"), "Latitude");
                Assert.That(location.Longitude, Is.EqualTo("5.678"), "Longitude");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            Assert.That(new Location().ToString(), Is.EqualTo("Latitude = 0.000; Longitude = 0.000"));
        }
        #endregion
    }
}
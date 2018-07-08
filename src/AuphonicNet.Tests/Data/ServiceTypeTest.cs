using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class ServiceTypeTest : TestBase<ServiceType>
    {
        #region Constructor
        public ServiceTypeTest()
            : base("service_type.json")
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
                Assert.That(Item.DisplayName, Is.EqualTo("display_name"), "DisplayName");
                Assert.That(Item.Parameters.Count, Is.EqualTo(0), "Parameters");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"display_name\":"), Is.True, "display_name");
                Assert.That(json.Contains("\"parameters\":"), Is.True, "parameters");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            ServiceType serviceType = null;

            Assert.That(() => serviceType = new ServiceType(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(serviceType.DisplayName, Is.Null, "DisplayName");
                Assert.That(serviceType.Parameters, Is.Null, "Parameters");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var serviceType = new ServiceType
            {
                DisplayName = "DisplayName"
            };

            Assert.That(serviceType.ToString(), Is.EqualTo(serviceType.DisplayName));
        }
        #endregion
    }
}
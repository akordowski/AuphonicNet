using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class InfoTest : TestBase<Info>
    {
        #region Constructor
        public InfoTest()
            : base("info.json")
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
                Assert.That(Item.Algorithms, Is.Not.Null, "Algorithms");
                Assert.That(Item.FileEndings, Is.Not.Null, "FileEndings");
                Assert.That(Item.OutputFiles, Is.Not.Null, "OutputFiles");
                Assert.That(Item.ProductionStatus, Is.Not.Null, "ProductionStatus");
                Assert.That(Item.ServiceTypes, Is.Not.Null, "ServiceTypes");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"algorithms\":"), Is.True, "algorithms");
                Assert.That(json.Contains("\"file_endings\":"), Is.True, "file_endings");
                Assert.That(json.Contains("\"output_files\":"), Is.True, "output_files");
                Assert.That(json.Contains("\"production_status\":"), Is.True, "production_status");
                Assert.That(json.Contains("\"service_types\":"), Is.True, "service_types");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Info info = null;

            Assert.That(() => info = new Info(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(info.Algorithms, Is.Null, "Algorithms");
                Assert.That(info.FileEndings, Is.Null, "FileEndings");
                Assert.That(info.OutputFiles, Is.Null, "OutputFiles");
                Assert.That(info.ProductionStatus, Is.Null, "ProductionStatus");
                Assert.That(info.ServiceTypes, Is.Null, "ServiceTypes");
            });
        }
        #endregion
    }
}
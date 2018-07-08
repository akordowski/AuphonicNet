using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class ServiceTest : TestBase<Service>
    {
        #region Constructor
        public ServiceTest()
            : base("service.json")
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
                Assert.That(Item.BaseUrl, Is.EqualTo("base_url"), "BaseUrl");
                Assert.That(Item.Bucket, Is.EqualTo("bucket"), "Bucket");
                Assert.That(Item.CannedAcl, Is.EqualTo("canned_acl"), "CannedAcl");
                Assert.That(Item.DisplayName, Is.EqualTo("display_name"), "DisplayName");
                Assert.That(Item.Email, Is.EqualTo("email"), "Email");
                Assert.That(Item.Host, Is.EqualTo("host"), "Host");
                Assert.That(Item.Incoming, Is.True, "Incoming");
                Assert.That(Item.KeyPrefix, Is.EqualTo("key_prefix"), "KeyPrefix");
                Assert.That(Item.LibsynDirectory, Is.EqualTo("libsyn_directory"), "LibsynDirectory");
                Assert.That(Item.LibsynShowSlug, Is.EqualTo("libsyn_show_slug"), "LibsynShowSlug");
                Assert.That(Item.Outgoing, Is.True, "Outgoing");
                Assert.That(Item.Path, Is.EqualTo("path"), "Path");
                Assert.That(Item.Permissions, Is.EqualTo("permissions"), "Permissions");
                Assert.That(Item.Port, Is.EqualTo("port"), "Port");
                Assert.That(Item.ProgramKeyword, Is.EqualTo("program_keyword"), "ProgramKeyword");
                Assert.That(Item.Type, Is.EqualTo("type"), "Type");
                Assert.That(Item.Url, Is.EqualTo("url"), "Url");
                Assert.That(Item.Uuid, Is.EqualTo("uuid"), "DisplayName");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"base_url\":"), Is.True, "base_url");
                Assert.That(json.Contains("\"bucket\":"), Is.True, "bucket");
                Assert.That(json.Contains("\"canned_acl\":"), Is.True, "canned_acl");
                Assert.That(json.Contains("\"display_name\":"), Is.True, "display_name");
                Assert.That(json.Contains("\"email\":"), Is.True, "email");
                Assert.That(json.Contains("\"host\":"), Is.True, "host");
                Assert.That(json.Contains("\"incoming\":"), Is.True, "incoming");
                Assert.That(json.Contains("\"key_prefix\":"), Is.True, "key_prefix");
                Assert.That(json.Contains("\"libsyn_directory\":"), Is.True, "libsyn_directory");
                Assert.That(json.Contains("\"libsyn_show_slug\":"), Is.True, "libsyn_show_slug");
                Assert.That(json.Contains("\"outgoing\":"), Is.True, "outgoing");
                Assert.That(json.Contains("\"path\":"), Is.True, "path");
                Assert.That(json.Contains("\"permissions\":"), Is.True, "permissions");
                Assert.That(json.Contains("\"port\":"), Is.True, "port");
                Assert.That(json.Contains("\"program_keyword\":"), Is.True, "program_keyword");
                Assert.That(json.Contains("\"type\":"), Is.True, "type");
                Assert.That(json.Contains("\"url\":"), Is.True, "url");
                Assert.That(json.Contains("\"uuid\":"), Is.True, "uuid");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            Service service = null;

            Assert.That(() => service = new Service(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(service.BaseUrl, Is.Null, "BaseUrl");
                Assert.That(service.Bucket, Is.Null, "Bucket");
                Assert.That(service.CannedAcl, Is.Null, "CannedAcl");
                Assert.That(service.DisplayName, Is.Null, "DisplayName");
                Assert.That(service.Email, Is.Null, "Email");
                Assert.That(service.Host, Is.Null, "Host");
                Assert.That(service.Incoming, Is.False, "Incoming");
                Assert.That(service.KeyPrefix, Is.Null, "KeyPrefix");
                Assert.That(service.LibsynDirectory, Is.Null, "LibsynDirectory");
                Assert.That(service.LibsynShowSlug, Is.Null, "LibsynShowSlug");
                Assert.That(service.Outgoing, Is.False, "Outgoing");
                Assert.That(service.Path, Is.Null, "Path");
                Assert.That(service.Permissions, Is.Null, "Permissions");
                Assert.That(service.Port, Is.Null, "Port");
                Assert.That(service.ProgramKeyword, Is.Null, "ProgramKeyword");
                Assert.That(service.Type, Is.Null, "Type");
                Assert.That(service.Url, Is.Null, "Url");
                Assert.That(service.Uuid, Is.Null, "DisplayName");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var service = new Service
            {
                DisplayName = "DisplayName"
            };

            Assert.That(service.ToString(), Is.EqualTo(service.DisplayName));
        }
        #endregion
    }
}
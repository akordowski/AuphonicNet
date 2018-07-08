using AuphonicNet.Data;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class OutgoingServiceTest : TestBase<OutgoingService>
    {
        #region Constructor
        public OutgoingServiceTest()
            : base("outgoing_service.json")
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
                Assert.That(Item.Category, Is.EqualTo("category"), "Category");
                Assert.That(Item.DisplayName, Is.EqualTo("display_name"), "DisplayName");
                Assert.That(Item.Downloadable, Is.True, "Downloadable");
                Assert.That(Item.Email, Is.EqualTo("email"), "Email");
                Assert.That(Item.ErrorMessage, Is.EqualTo("error_message"), "ErrorMessage");
                Assert.That(Item.Host, Is.EqualTo("host"), "Host");
                Assert.That(Item.Incomming, Is.True, "Incomming");
                Assert.That(Item.Outgoing, Is.True, "Outgoing");
                Assert.That(Item.Path, Is.EqualTo("path"), "Path");
                Assert.That(Item.Port, Is.EqualTo(21), "Port");
                Assert.That(Item.Privacy, Is.EqualTo("privacy"), "Privacy");
                Assert.That(Item.ResultPage, Is.EqualTo("result_page"), "ResultPage");
                Assert.That(Item.ResultUrls.Count, Is.EqualTo(0), "ResultUrls");
                Assert.That(Item.Sharing, Is.EqualTo("sharing"), "Sharing");
                Assert.That(Item.TrackType, Is.EqualTo("track_type"), "TrackType");
                Assert.That(Item.TransferSuccess, Is.True, "TransferSuccess");
                Assert.That(Item.Type, Is.EqualTo("type"), "Type");
                Assert.That(Item.Uuid, Is.EqualTo("uuid"), "Uuid");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"base_url\":"), Is.False, "base_url");
                Assert.That(json.Contains("\"category\":"), Is.False, "category");
                Assert.That(json.Contains("\"display_name\":"), Is.False, "display_name");
                Assert.That(json.Contains("\"downloadable\":"), Is.False, "downloadable");
                Assert.That(json.Contains("\"email\":"), Is.False, "email");
                Assert.That(json.Contains("\"error_message\":"), Is.False, "error_message");
                Assert.That(json.Contains("\"host\":"), Is.False, "host");
                Assert.That(json.Contains("\"incomming\":"), Is.False, "incomming");
                Assert.That(json.Contains("\"outgoing\":"), Is.False, "outgoing");
                Assert.That(json.Contains("\"path\":"), Is.False, "path");
                Assert.That(json.Contains("\"port\":"), Is.False, "port");
                Assert.That(json.Contains("\"privacy\":"), Is.False, "privacy");
                Assert.That(json.Contains("\"result_page\":"), Is.False, "result_page");
                Assert.That(json.Contains("\"result_urls\":"), Is.False, "result_urls");
                Assert.That(json.Contains("\"sharing\":"), Is.False, "sharing");
                Assert.That(json.Contains("\"track_type\":"), Is.False, "track_type");
                Assert.That(json.Contains("\"transfer_success\":"), Is.False, "transfer_success");
                Assert.That(json.Contains("\"type\":"), Is.False, "type");
                Assert.That(json.Contains("\"uuid\":"), Is.True, "uuid");
            });
        }

        [Test]
        public void Initialize_Constructor_1()
        {
            OutgoingService outgoingService = null;

            Assert.That(() => outgoingService = new OutgoingService(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(outgoingService.BaseUrl, Is.Null, "BaseUrl");
                Assert.That(outgoingService.Category, Is.Null, "Category");
                Assert.That(outgoingService.DisplayName, Is.Null, "DisplayName");
                Assert.That(outgoingService.Downloadable, Is.False, "Downloadable");
                Assert.That(outgoingService.Email, Is.Null, "Email");
                Assert.That(outgoingService.ErrorMessage, Is.Null, "ErrorMessage");
                Assert.That(outgoingService.Host, Is.Null, "Host");
                Assert.That(outgoingService.Incomming, Is.False, "Incomming");
                Assert.That(outgoingService.Outgoing, Is.False, "Outgoing");
                Assert.That(outgoingService.Path, Is.Null, "Path");
                Assert.That(outgoingService.Port, Is.EqualTo(0), "Port");
                Assert.That(outgoingService.Privacy, Is.Null, "Privacy");
                Assert.That(outgoingService.ResultPage, Is.Null, "ResultPage");
                Assert.That(outgoingService.ResultUrls, Is.Null, "ResultUrls");
                Assert.That(outgoingService.Sharing, Is.Null, "Sharing");
                Assert.That(outgoingService.TrackType, Is.Null, "TrackType");
                Assert.That(outgoingService.TransferSuccess, Is.False, "TransferSuccess");
                Assert.That(outgoingService.Type, Is.Null, "Type");
                Assert.That(outgoingService.Uuid, Is.Null, "Uuid");
            });
        }

        [Test]
        public void Initialize_Constructor_2(
            [Values(null, "", "  ", "uuid")] string uuid)
        {
            Type expectedException = null;
            string expectedParamName = null;

            if (String.IsNullOrWhiteSpace(uuid))
            {
                expectedException = uuid == null ? typeof(ArgumentNullException) : typeof(ArgumentException);
                expectedParamName = nameof(uuid);
            }

            if (expectedException == null)
            {
                OutgoingService outgoingService = null;

                Assert.That(() => outgoingService = new OutgoingService(uuid), Throws.Nothing);
                Assert.Multiple(() =>
                {
                    Assert.That(outgoingService.BaseUrl, Is.Null, "BaseUrl");
                    Assert.That(outgoingService.Category, Is.Null, "Category");
                    Assert.That(outgoingService.DisplayName, Is.Null, "DisplayName");
                    Assert.That(outgoingService.Downloadable, Is.False, "Downloadable");
                    Assert.That(outgoingService.Email, Is.Null, "Email");
                    Assert.That(outgoingService.ErrorMessage, Is.Null, "ErrorMessage");
                    Assert.That(outgoingService.Host, Is.Null, "Host");
                    Assert.That(outgoingService.Incomming, Is.False, "Incomming");
                    Assert.That(outgoingService.Outgoing, Is.False, "Outgoing");
                    Assert.That(outgoingService.Path, Is.Null, "Path");
                    Assert.That(outgoingService.Port, Is.EqualTo(0), "Port");
                    Assert.That(outgoingService.Privacy, Is.Null, "Privacy");
                    Assert.That(outgoingService.ResultPage, Is.Null, "ResultPage");
                    Assert.That(outgoingService.ResultUrls, Is.Null, "ResultUrls");
                    Assert.That(outgoingService.Sharing, Is.Null, "Sharing");
                    Assert.That(outgoingService.TrackType, Is.Null, "TrackType");
                    Assert.That(outgoingService.TransferSuccess, Is.False, "TransferSuccess");
                    Assert.That(outgoingService.Type, Is.Null, "Type");
                    Assert.That(outgoingService.Uuid, Is.EqualTo(uuid), "Uuid");
                });
            }
            else
            {
                Assert.That(() => new OutgoingService(uuid), Throws
                    .Exception.TypeOf(expectedException)
                    .With.Property("ParamName").EqualTo(expectedParamName));
            }
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var outgoingService = new OutgoingService
            {
                DisplayName = "DisplayName"
            };

            Assert.That(outgoingService.ToString(), Is.EqualTo(outgoingService.DisplayName));
        }
        #endregion
    }
}
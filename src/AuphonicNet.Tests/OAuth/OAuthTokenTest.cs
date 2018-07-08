using AuphonicNet.OAuth;
using NUnit.Framework;
using System;

namespace AuphonicNet.Tests.Data
{
    [TestFixture]
    public class OAuthTokenTest : TestBase<OAuthToken>
    {
        #region Constructor
        public OAuthTokenTest()
            : base("oauth_token.json")
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
                Assert.That(Item.AccessToken, Is.EqualTo("access_token"), "AccessToken");
                Assert.That(Item.TokenType, Is.EqualTo("token_type"), "TokenType");
                Assert.That(Item.ExpiresIn, Is.EqualTo(0), "ExpiresIn");
                Assert.That(Item.Username, Is.EqualTo("username"), "Username");
                Assert.That(Item.Scope, Is.EqualTo("scope"), "Scope");
            });
        }

        [Test, Order(2)]
        public void Serialize_Returns_Valid_Result()
        {
            var json = Serialize();

            Assert.That(String.IsNullOrWhiteSpace(json), Is.False);
            Assert.Multiple(() =>
            {
                Assert.That(json.Contains("\"access_token\":"), Is.True, "access_token");
                Assert.That(json.Contains("\"token_type\":"), Is.True, "token_type");
                Assert.That(json.Contains("\"expires_in\":"), Is.True, "expires_in");
                Assert.That(json.Contains("\"user_name\":"), Is.True, "user_name");
                Assert.That(json.Contains("\"scope\":"), Is.True, "scope");
            });
        }

        [Test]
        public void Initialize_Constructor()
        {
            OAuthToken token = null;

            Assert.That(() => token = new OAuthToken(), Throws.Nothing);
            Assert.Multiple(() =>
            {
                Assert.That(token.AccessToken, Is.Null, "AccessToken");
                Assert.That(token.TokenType, Is.Null, "TokenType");
                Assert.That(token.ExpiresIn, Is.EqualTo(0), "ExpiresIn");
                Assert.That(token.Username, Is.Null, "Username");
                Assert.That(token.Scope, Is.Null, "Scope");
            });
        }

        [Test]
        public void Invoke_ToString_Returns_Valid_Result()
        {
            var token = new OAuthToken();
            token.AccessToken = "AccessToken";
            token.TokenType = "TokenType";

            Assert.That(token.ToString(), Is.EqualTo($"AccessToken = {token.AccessToken}; TokenType = {token.TokenType}"));
        }
        #endregion
    }
}
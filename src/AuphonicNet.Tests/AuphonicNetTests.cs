using AuphonicNet.Data;
using NUnit.Framework;
using System;
using System.Collections;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;
using static NUnit.Framework.TestContext;

namespace AuphonicNet.Tests
{
    [TestFixture]
    public class AuphonicNetTests
    {
        #region Private Fields
        private Auphonic _auphonic;
        private string _auphonicExceptionAuthenticationMessage = "No authentication credentials provided.";
        #endregion

        #region SetUp
        [SetUp]
        public void SetUp()
        {
            _auphonic = new Auphonic("clientId", "clientSecret");

            if (!CurrentContext.Test.FullName.Contains("Without_Authentication"))
            {
                _auphonic.Authenticate("token");
            }
        }
        #endregion


        #region Tests - Auphonic
        [TestCase("", "", "clientId")]
        [TestCase("  ", "", "clientId")]
        [TestCase("ClientId", "", "clientSecret")]
        [TestCase("ClientId", "  ", "clientSecret")]
        public void Initialize_Auphonic_With_Empty_Values_Throws_ArgumentException(string clientId, string clientSecret, string paramName)
        {
            Assert.That(() => new Auphonic(clientId, clientSecret), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCase(null, null, "clientId")]
        [TestCase("ClientId", null, "clientSecret")]
        public void Initialize_Auphonic_With_Null_Values_Throws_ArgumentNullException(string clientId, string clientSecret, string paramName)
        {
            Assert.That(() => new Auphonic(clientId, clientSecret), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [Test]
        public void Initialize_Auphonic_With_Valid()
        {
            Auphonic auphonic = null;
            string clientId = Guid.NewGuid().ToString();
            string clientSecret = Guid.NewGuid().ToString();

            Assert.That(() => auphonic = new Auphonic(clientId, clientSecret), Throws.Nothing);
            Assert.That(auphonic.AccessToken, Is.Null);
            Assert.That(auphonic.BaseUrl, Is.EqualTo("https://auphonic.com"));
            Assert.That(auphonic.ClientId, Is.EqualTo(clientId));
            Assert.That(auphonic.ClientSecret, Is.EqualTo(clientSecret));
        }
        #endregion

        #region Tests - Authenticate
        [Test]
        public void Invoke_Authenticate_With_Empty_AccessToken_Throws_ArgumentException()
        {
            Assert.That(() => _auphonic.Authenticate(String.Empty), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("accessToken"));
        }

        [Test]
        public void Invoke_Authenticate_With_Null_AccessToken_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.Authenticate(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("accessToken"));
        }

        [Test]
        public void Invoke_Authenticate_With_AccessToken_Sets_AccessToken_Property()
        {
            var accessToken = Guid.NewGuid().ToString();

            Assert.That(() => _auphonic.Authenticate(accessToken), Throws.Nothing);
            Assert.That(_auphonic.AccessToken, Is.EqualTo(accessToken));
        }

        [TestCase("", "", "username")]
        [TestCase("  ", "", "username")]
        [TestCase("username", "", "password")]
        [TestCase("username", "  ", "password")]
        public void Invoke_Authenticate_With_Empty_Values_Throws_ArgumentException(string username, string password, string paramName)
        {
            var accessToken = Guid.NewGuid().ToString();

            Assert.That(() => _auphonic.Authenticate(username, password), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCase(null, null, "username")]
        [TestCase("username", null, "password")]
        public void Invoke_Authenticate_With_Null_Values_Throws_ArgumentNullException(string username, string password, string paramName)
        {
            var accessToken = Guid.NewGuid().ToString();

            Assert.That(() => _auphonic.Authenticate(username, password), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCase("", "", "username")]
        [TestCase("  ", "", "username")]
        [TestCase("username", "", "password")]
        [TestCase("username", "  ", "password")]
        public void Invoke_AuthenticateAsync_With_Empty_Values_Throws_ArgumentException(string username, string password, string paramName)
        {
            var accessToken = Guid.NewGuid().ToString();

            Assert.That(async () => await _auphonic.AuthenticateAsync(username, password), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCase(null, null, "username")]
        [TestCase("username", null, "password")]
        public void Invoke_AuthenticateAsync_With_Null_Values_Throws_ArgumentNullException(string username, string password, string paramName)
        {
            var accessToken = Guid.NewGuid().ToString();

            Assert.That(async () => await _auphonic.AuthenticateAsync(username, password), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCase("", "", "username")]
        [TestCase("  ", "", "username")]
        [TestCase("username", "", "password")]
        [TestCase("username", "  ", "password")]
        public void Invoke_AuthenticateAsync_With_CancellationToken_And_Empty_Values_Throws_ArgumentException(string username, string password, string paramName)
        {
            var accessToken = Guid.NewGuid().ToString();

            Assert.That(async () => await _auphonic.AuthenticateAsync(username, password, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCase(null, null, "username")]
        [TestCase("username", null, "password")]
        public void Invoke_AuthenticateAsync_With_CancellationToken_And_Null_Values_Throws_ArgumentNullException(string username, string password, string paramName)
        {
            var accessToken = Guid.NewGuid().ToString();

            Assert.That(async () => await _auphonic.AuthenticateAsync(username, password, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }
        #endregion

        #region Tests - GetAccountInfo
        [Test]
        public void Invoke_GetAccountInfo_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetAccountInfo());
        }

        [Test]
        public void Invoke_GetAccountInfoAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetAccountInfoAsync());
        }

        [Test]
        public void Invoke_GetAccountInfoAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetAccountInfoAsync(CancellationToken.None));
        }
        #endregion


        #region Tests - CreatePreset
        [Test]
        public void Invoke_CreatePreset_With_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.CreatePreset(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_CreatePresetAsync_With_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.CreatePresetAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_CreatePresetAsync_With_CancellationToken_And_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.CreatePresetAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_CreatePreset_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.CreatePreset(new Preset()));
        }

        [Test]
        public void Invoke_CreatePresetAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.CreatePresetAsync(new Preset()));
        }

        [Test]
        public void Invoke_CreatePresetAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.CreatePresetAsync(new Preset(), CancellationToken.None));
        }
        #endregion

        #region Tests - DeletePreset
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeletePreset_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.DeletePreset(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [Test]
        public void Invoke_DeletePreset_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.DeletePreset(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeletePresetAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeletePresetAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [Test]
        public void Invoke_DeletePresetAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeletePresetAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeletePresetAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeletePresetAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [Test]
        public void Invoke_DeletePresetAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeletePresetAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [Test]
        public void Invoke_DeletePreset_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.DeletePreset("uuid"));
        }

        [Test]
        public void Invoke_DeletePresetAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeletePresetAsync("uuid"));
        }

        [Test]
        public void Invoke_DeletePresetAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeletePresetAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - GetPreset
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetPreset_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.GetPreset(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [Test]
        public void Invoke_GetPreset_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.GetPreset(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetPresetAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.GetPresetAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [Test]
        public void Invoke_GetPresetAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.GetPresetAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetPresetAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.GetPresetAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [Test]
        public void Invoke_GetPresetAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.GetPresetAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("presetUuid"));
        }

        [Test]
        public void Invoke_GetPreset_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetPreset("uuid"));
        }

        [Test]
        public void Invoke_GetPresetAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetPresetAsync("uuid"));
        }

        [Test]
        public void Invoke_GetPresetAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetPresetAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - GetPresets
        [TestCaseSource("TestCaseSource_Invalid_Limit")]
        public void Invoke_GetPresets_With_Invalid_Limit_Throws_ArgumentOutOfRangeException(int limit, string paramName)
        {
            Assert.That(() => _auphonic.GetPresets(limit), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit_And_Offset")]
        public void Invoke_GetPresets_With_Invalid_Limit_And_Offset_Throws_ArgumentOutOfRangeException(int limit, int offset, string paramName)
        {
            Assert.That(() => _auphonic.GetPresets(limit, offset), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit")]
        public void Invoke_GetPresetsAsync_With_Invalid_Limit_Throws_ArgumentOutOfRangeException(int limit, string paramName)
        {
            Assert.That(async () => await _auphonic.GetPresetsAsync(limit), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit_And_Offset")]
        public void Invoke_GetPresetsAsync_With_Invalid_Limit_And_Offset_Throws_ArgumentOutOfRangeException(int limit, int offset, string paramName)
        {
            Assert.That(async () => await _auphonic.GetPresetsAsync(limit, offset), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit")]
        public void Invoke_GetPresetsAsync_With_CancellationToken_And_Invalid_Limit_Throws_ArgumentOutOfRangeException(int limit, string paramName)
        {
            Assert.That(async () => await _auphonic.GetPresetsAsync(limit, CancellationToken.None), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit_And_Offset")]
        public void Invoke_GetPresetsAsync_With_CancellationToken_And_Invalid_Limit_And_Offset_Throws_ArgumentOutOfRangeException(int limit, int offset, string paramName)
        {
            Assert.That(async () => await _auphonic.GetPresetsAsync(limit, offset, CancellationToken.None), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [Test]
        public void Invoke_GetPresets_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetPresets());
        }

        [Test]
        public void Invoke_GetPresetsAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetPresetsAsync());
        }

        [Test]
        public void Invoke_GetPresetsAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetPresetsAsync(CancellationToken.None));
        }

        [Test]
        public void Invoke_GetPresetsUuids_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetPresetsUuids());
        }

        [Test]
        public void Invoke_GetPresetsUuidsAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetPresetsUuidsAsync());
        }

        [Test]
        public void Invoke_GetPresetsUuidsAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetPresetsUuidsAsync(CancellationToken.None));
        }
        #endregion

        #region Tests - UpdatePreset
        [Test]
        public void Invoke_UpdatePreset_With_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.UpdatePreset(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_UpdatePreset_With_New_Preset_Throws_ArgumentException()
        {
            Assert.That(() => _auphonic.UpdatePreset(new Preset()), Throws
                .ArgumentException
                .And.Message.Contains("Preset UUID cannot be null or empty.")
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_UpdatePresetAsync_With_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.UpdatePresetAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_UpdatePresetAsync_With_New_Preset_Throws_ArgumentException()
        {
            Assert.That(() => _auphonic.UpdatePresetAsync(new Preset()), Throws
                .ArgumentException
                .And.Message.Contains("Preset UUID cannot be null or empty.")
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_UpdatePresetAsync_With_CancellationToken_And_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.UpdatePresetAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_UpdatePresetAsync_With_CancellationToken_And_New_Preset_Throws_ArgumentException()
        {
            Assert.That(() => _auphonic.UpdatePresetAsync(new Preset(), CancellationToken.None), Throws
                .ArgumentException
                .And.Message.Contains("Preset UUID cannot be null or empty.")
                .And.Property("ParamName").EqualTo("preset"));
        }

        [Test]
        public void Invoke_UpdatePreset_Without_Authentication_Throws_AuphonicException()
        {
            var preset = new Preset();
            preset.Uuid = "uuid";

            InvokeWithoutAuthentication(() => _auphonic.UpdatePreset(preset));
        }

        [Test]
        public void Invoke_UpdatePresetAsync_Without_Authentication_Throws_AuphonicException()
        {
            var preset = new Preset();
            preset.Uuid = "uuid";

            InvokeWithoutAuthenticationAsync(async () => await _auphonic.UpdatePresetAsync(preset));
        }

        [Test]
        public void Invoke_UpdatePresetAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            var preset = new Preset();
            preset.Uuid = "uuid";

            InvokeWithoutAuthenticationAsync(async () => await _auphonic.UpdatePresetAsync(preset, CancellationToken.None));
        }
        #endregion


        #region Tests - CreateProduction
        [Test]
        public void Invoke_CreateProduction_With_Null_Production_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.CreateProduction(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_CreateProductionAsync_With_Null_Production_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.CreateProductionAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_CreateProductionAsync_With_CancellationToken_And_Null_Production_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.CreateProductionAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_CreateProduction_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.CreateProduction(new Production()));
        }

        [Test]
        public void Invoke_CreateProductionAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.CreateProductionAsync(new Production()));
        }

        [Test]
        public void Invoke_CreateProductionAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.CreateProductionAsync(new Production(), CancellationToken.None));
        }
        #endregion

        #region Tests - DeleteProduction
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProduction_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.DeleteProduction(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProduction_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.DeleteProduction(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProduction_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.DeleteProduction("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionAsync("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - DeleteProductionChapters
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionChapters_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.DeleteProductionChapters(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionChapters_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.DeleteProductionChapters(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionChaptersAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionChaptersAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionChaptersAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionChaptersAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionChaptersAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionChaptersAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionChaptersAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionChaptersAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionChapters_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.DeleteProductionChapters("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionChaptersAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionChaptersAsync("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionChaptersAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionChaptersAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - DeleteProductionCoverImage
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionCoverImage_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.DeleteProductionCoverImage(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionCoverImage_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.DeleteProductionCoverImage(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionCoverImageAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionCoverImageAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionCoverImageAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionCoverImageAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionCoverImageAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionCoverImageAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionCoverImageAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionCoverImageAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionCoverImage_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.DeleteProductionCoverImage("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionCoverImageAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionCoverImageAsync("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionCoverImageAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionCoverImageAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - DeleteProductionOutputFiles
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionOutputFiles_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.DeleteProductionOutputFiles(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionOutputFiles_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.DeleteProductionOutputFiles(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionOutputFilesAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionOutputFilesAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionOutputFilesAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionOutputFilesAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionOutputFilesAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionOutputFilesAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionOutputFilesAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionOutputFilesAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionOutputFiles_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.DeleteProductionOutputFiles("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionOutputFilesAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionOutputFilesAsync("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionOutputFilesAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionOutputFilesAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - DeleteProductionSpeechRecognition
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionSpeechRecognition_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.DeleteProductionSpeechRecognition(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionSpeechRecognition_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.DeleteProductionSpeechRecognition(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionSpeechRecognitionAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionSpeechRecognitionAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionSpeechRecognitionAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionSpeechRecognitionAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_DeleteProductionSpeechRecognitionAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.DeleteProductionSpeechRecognitionAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionSpeechRecognitionAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.DeleteProductionSpeechRecognitionAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_DeleteProductionSpeechRecognition_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.DeleteProductionSpeechRecognition("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionSpeechRecognitionAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionSpeechRecognitionAsync("uuid"));
        }

        [Test]
        public void Invoke_DeleteProductionSpeechRecognitionAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.DeleteProductionSpeechRecognitionAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - GetProduction
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetProduction_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.GetProduction(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_GetProduction_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.GetProduction(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetProductionAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.GetProductionAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_GetProductionAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.GetProductionAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetProductionAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.GetProductionAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_GetProductionAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.GetProductionAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_GetProduction_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetProduction("uuid"));
        }

        [Test]
        public void Invoke_GetProductionAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetProductionAsync("uuid"));
        }

        [Test]
        public void Invoke_GetProductionAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetProductionAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - GetProductions
        [TestCaseSource("TestCaseSource_Invalid_Limit")]
        public void Invoke_GetProductions_With_Invalid_Limit_Throws_ArgumentOutOfRangeException(int limit, string paramName)
        {
            Assert.That(() => _auphonic.GetProductions(limit), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit_And_Offset")]
        public void Invoke_GetProductions_With_Invalid_Limit_And_Offset_Throws_ArgumentOutOfRangeException(int limit, int offset, string paramName)
        {
            Assert.That(() => _auphonic.GetProductions(limit, offset), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit")]
        public void Invoke_GetProductionsAsync_With_Invalid_Limit_Throws_ArgumentOutOfRangeException(int limit, string paramName)
        {
            Assert.That(async () => await _auphonic.GetProductionsAsync(limit), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit_And_Offset")]
        public void Invoke_GetProductionsAsync_With_Invalid_Limit_And_Offset_Throws_ArgumentOutOfRangeException(int limit, int offset, string paramName)
        {
            Assert.That(async () => await _auphonic.GetProductionsAsync(limit, offset), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit")]
        public void Invoke_GetProductionsAsync_With_CancellationToken_And_Invalid_Limit_Throws_ArgumentOutOfRangeException(int limit, string paramName)
        {
            Assert.That(async () => await _auphonic.GetProductionsAsync(limit, CancellationToken.None), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_Invalid_Limit_And_Offset")]
        public void Invoke_GetProductionsAsync_With_CancellationToken_And_Invalid_Limit_And_Offset_Throws_ArgumentOutOfRangeException(int limit, int offset, string paramName)
        {
            Assert.That(async () => await _auphonic.GetProductionsAsync(limit, offset, CancellationToken.None), Throws
                .InstanceOf<ArgumentOutOfRangeException>()
                .And.Property("ParamName").EqualTo(paramName));
        }

        [Test]
        public void Invoke_GetProductions_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetProductions());
        }

        [Test]
        public void Invoke_GetProductionsAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetProductionsAsync());
        }

        [Test]
        public void Invoke_GetProductionsAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetProductionsAsync(CancellationToken.None));
        }
        #endregion

        #region Tests - GetProductionsUuids
        [Test]
        public void Invoke_GetProductionsUuids_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetProductionsUuids());
        }

        [Test]
        public void Invoke_GetProductionsUuidsAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetProductionsUuidsAsync());
        }

        [Test]
        public void Invoke_GetProductionsUuidsAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetProductionsUuidsAsync(CancellationToken.None));
        }
        #endregion

        #region Tests - SetProductionCoverImageFromPreset
        [TestCaseSource("TestCaseSource_SetProductionCoverImageFromPreset_Empty_Values")]
        public void Invoke_SetProductionCoverImageFromPreset_With_Empty_Values_Throws_ArgumentException(string uuid, string path, string paramName)
        {
            Assert.That(() => _auphonic.SetProductionCoverImage(uuid, path), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_SetProductionCoverImageFromPreset_Null_Values")]
        public void Invoke_SetProductionCoverImageFromPreset_With_Empty_Values_Throws_ArgumentNullException(string uuid, string path, string paramName)
        {
            Assert.That(() => _auphonic.SetProductionCoverImageFromPresetAsync(uuid, path), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_SetProductionCoverImageFromPreset_Empty_Values")]
        public void Invoke_SetProductionCoverImageFromPresetAsync_With_Empty_Values_Throws_ArgumentException(string uuid, string path, string paramName)
        {
            Assert.That(async () => await _auphonic.SetProductionCoverImageFromPresetAsync(uuid, path), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_SetProductionCoverImageFromPreset_Null_Values")]
        public void Invoke_SetProductionCoverImageFromPresetAsync_With_Empty_Values_Throws_ArgumentNullException(string uuid, string path, string paramName)
        {
            Assert.That(async () => await _auphonic.SetProductionCoverImageFromPresetAsync(uuid, path), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_SetProductionCoverImageFromPreset_Empty_Values")]
        public void Invoke_SetProductionCoverImageFromPresetAsync_With_CancellationToken_And_Empty_Values_Throws_ArgumentException(string uuid, string path, string paramName)
        {
            Assert.That(async () => await _auphonic.SetProductionCoverImageFromPresetAsync(uuid, path, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_SetProductionCoverImageFromPreset_Null_Values")]
        public void Invoke_SetProductionCoverImageFromPresetAsync_With_CancellationToken_And_Empty_Values_Throws_ArgumentNullException(string uuid, string path, string paramName)
        {
            Assert.That(async () => await _auphonic.SetProductionCoverImageFromPresetAsync(uuid, path, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [Test]
        public void Invoke_SetProductionCoverImageFromPreset_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.SetProductionCoverImage("productionUuid", "presetUuid"));
        }

        [Test]
        public void Invoke_SetProductionCoverImageFromPresetAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.SetProductionCoverImageFromPresetAsync("productionUuid", "presetUuid"));
        }

        [Test]
        public void Invoke_SetProductionCoverImageFromPresetAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.SetProductionCoverImageFromPresetAsync("productionUuid", "presetUuid", CancellationToken.None));
        }
        #endregion

        #region Tests - StartProduction
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_StartProduction_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.StartProduction(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_StartProduction_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.StartProduction(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_StartProductionAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.StartProductionAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_StartProductionAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.StartProductionAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_StartProductionAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.StartProductionAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_StartProductionAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.StartProductionAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_StartProduction_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.StartProduction("uuid"));
        }

        [Test]
        public void Invoke_StartProductionAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.StartProductionAsync("uuid"));
        }

        [Test]
        public void Invoke_StartProductionAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.StartProductionAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - StopProduction
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_StopProduction_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.StopProduction(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_StopProduction_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.StopProduction(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_StopProductionAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.StopProductionAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_StopProductionAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.StopProductionAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_StopProductionAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.StopProductionAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_StopProductionAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.StopProductionAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("productionUuid"));
        }

        [Test]
        public void Invoke_StopProduction_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.StopProduction("uuid"));
        }

        [Test]
        public void Invoke_StopProductionAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.StopProductionAsync("uuid"));
        }

        [Test]
        public void Invoke_StopProductionAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.StopProductionAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - UpdateProduction
        [Test]
        public void Invoke_UpdateProduction_With_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.UpdateProduction(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_UpdateProduction_With_New_Preset_Throws_ArgumentException()
        {
            Assert.That(() => _auphonic.UpdateProduction(new Production()), Throws
                .ArgumentException
                .And.Message.Contains("Production UUID cannot be null or empty.")
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_UpdateProductionAsync_With_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.UpdateProductionAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_UpdateProductionAsync_With_New_Preset_Throws_ArgumentException()
        {
            Assert.That(() => _auphonic.UpdateProductionAsync(new Production()), Throws
                .ArgumentException
                .And.Message.Contains("Production UUID cannot be null or empty.")
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_UpdateProductionAsync_With_CancellationToken_And_Null_Preset_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.UpdateProductionAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_UpdateProductionAsync_With_CancellationToken_And_New_Preset_Throws_ArgumentException()
        {
            Assert.That(() => _auphonic.UpdateProductionAsync(new Production(), CancellationToken.None), Throws
                .ArgumentException
                .And.Message.Contains("Production UUID cannot be null or empty.")
                .And.Property("ParamName").EqualTo("production"));
        }

        [Test]
        public void Invoke_UpdateProduction_Without_Authentication_Throws_AuphonicException()
        {
            var production = new Production();
            production.Uuid = "uuid";

            InvokeWithoutAuthentication(() => _auphonic.UpdateProduction(production));
        }

        [Test]
        public void Invoke_UpdateProductionAsync_Without_Authentication_Throws_AuphonicException()
        {
            var production = new Production();
            production.Uuid = "uuid";

            InvokeWithoutAuthenticationAsync(async () => await _auphonic.UpdateProductionAsync(production));
        }

        [Test]
        public void Invoke_UpdateProductionAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            var production = new Production();
            production.Uuid = "uuid";

            InvokeWithoutAuthenticationAsync(async () => await _auphonic.UpdateProductionAsync(production, CancellationToken.None));
        }
        #endregion

        #region Tests - UploadProductionFile
        [TestCaseSource("TestCaseSource_UploadProductionFile_Empty_Values")]
        public void Invoke_UploadProductionFile_With_Empty_Values_Throws_ArgumentException(string uuid, string path, string paramName)
        {
            Assert.That(() => _auphonic.UploadProductionFile(uuid, path), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_UploadProductionFile_Null_Values")]
        public void Invoke_UploadProductionFile_With_Empty_Values_Throws_ArgumentNullException(string uuid, string path, string paramName)
        {
            Assert.That(() => _auphonic.UploadProductionFileAsync(uuid, path), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_UploadProductionFile_Empty_Values")]
        public void Invoke_UploadProductionFileAsync_With_Empty_Values_Throws_ArgumentException(string uuid, string path, string paramName)
        {
            Assert.That(async () => await _auphonic.UploadProductionFileAsync(uuid, path), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_UploadProductionFile_Null_Values")]
        public void Invoke_UploadProductionFileAsync_With_Empty_Values_Throws_ArgumentNullException(string uuid, string path, string paramName)
        {
            Assert.That(async () => await _auphonic.UploadProductionFileAsync(uuid, path), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_UploadProductionFile_Empty_Values")]
        public void Invoke_UploadProductionFileAsync_With_CancellationToken_And_Empty_Values_Throws_ArgumentException(string uuid, string path, string paramName)
        {
            Assert.That(async () => await _auphonic.UploadProductionFileAsync(uuid, path, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [TestCaseSource("TestCaseSource_UploadProductionFile_Null_Values")]
        public void Invoke_UploadProductionFileAsync_With_CancellationToken_And_Empty_Values_Throws_ArgumentNullException(string uuid, string path, string paramName)
        {
            Assert.That(async () => await _auphonic.UploadProductionFileAsync(uuid, path, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo(paramName));
        }

        [Test]
        public void Invoke_UploadProductionFile_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.UploadProductionFile("uuid", "path"));
        }

        [Test]
        public void Invoke_UploadProductionFileAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.UploadProductionFileAsync("uuid", "path"));
        }

        [Test]
        public void Invoke_UploadProductionFileAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.UploadProductionFileAsync("uuid", "path", CancellationToken.None));
        }
        #endregion


        #region Tests - ServiceFiles
        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetServiceFiles_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(() => _auphonic.GetServiceFiles(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("serviceUuid"));
        }

        [Test]
        public void Invoke_GetServiceFiles_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(() => _auphonic.GetServiceFiles(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("serviceUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetServiceFilesAsync_With_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.GetServiceFilesAsync(uuid), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("serviceUuid"));
        }

        [Test]
        public void Invoke_GetServiceFilesAsync_With_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.GetServiceFilesAsync(null), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("serviceUuid"));
        }

        [TestCaseSource("TestCaseSource_Empty_Uuid")]
        public void Invoke_GetServiceFilesAsync_With_CancellationToken_And_Empty_Uuid_Throws_ArgumentException(string uuid)
        {
            Assert.That(async () => await _auphonic.GetServiceFilesAsync(uuid, CancellationToken.None), Throws
                .ArgumentException
                .And.Property("ParamName").EqualTo("serviceUuid"));
        }

        [Test]
        public void Invoke_GetServiceFilesAsync_With_CancellationToken_And_Null_Uuid_Throws_ArgumentNullException()
        {
            Assert.That(async () => await _auphonic.GetServiceFilesAsync(null, CancellationToken.None), Throws
                .ArgumentNullException
                .And.Property("ParamName").EqualTo("serviceUuid"));
        }

        [Test]
        public void Invoke_GetServiceFiles_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetServiceFiles("uuid"));
        }

        [Test]
        public void Invoke_GetServiceFilesAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetServiceFilesAsync("uuid"));
        }

        [Test]
        public void Invoke_GetServiceFilesAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetServiceFilesAsync("uuid", CancellationToken.None));
        }
        #endregion

        #region Tests - Services
        [Test]
        public void Invoke_GetServices_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthentication(() => _auphonic.GetServices());
        }

        [Test]
        public void Invoke_GetServicesAsync_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetServicesAsync());
        }

        [Test]
        public void Invoke_GetServicesAsync_With_CancellationToken_Without_Authentication_Throws_AuphonicException()
        {
            InvokeWithoutAuthenticationAsync(async () => await _auphonic.GetServicesAsync(CancellationToken.None));
        }
        #endregion


        #region Test Case Sources
        public static IEnumerable TestCaseSource_Empty_Uuid()
        {
            yield return new TestCaseData("");
            yield return new TestCaseData("  ");
        }

        public static IEnumerable TestCaseSource_Invalid_Limit()
        {
            yield return new TestCaseData(-1, "limit");
        }

        public static IEnumerable TestCaseSource_Invalid_Limit_And_Offset()
        {
            yield return new TestCaseData(-1, -1, "limit");
            yield return new TestCaseData(0, -1, "offset");
        }

        public static IEnumerable TestCaseSource_SetProductionCoverImageFromPreset_Empty_Values()
        {
            yield return new TestCaseData("", "", "productionUuid");
            yield return new TestCaseData("  ", "", "productionUuid");
            yield return new TestCaseData("productionUuid", "", "presetUuid");
            yield return new TestCaseData("productionUuid", "  ", "presetUuid");
        }

        public static IEnumerable TestCaseSource_SetProductionCoverImageFromPreset_Null_Values()
        {
            yield return new TestCaseData(null, null, "productionUuid");
            yield return new TestCaseData("productionUuid", null, "presetUuid");
        }

        public static IEnumerable TestCaseSource_UploadProductionFile_Empty_Values()
        {
            yield return new TestCaseData("", "", "productionUuid");
            yield return new TestCaseData("  ", "", "productionUuid");
            yield return new TestCaseData("uuid", "", "filePath");
            yield return new TestCaseData("uuid", "  ", "filePath");
        }

        public static IEnumerable TestCaseSource_UploadProductionFile_Null_Values()
        {
            yield return new TestCaseData(null, null, "productionUuid");
            yield return new TestCaseData("uuid", null, "filePath");
        }
        #endregion

        #region Private Methods
        private void InvokeWithoutAuthentication(Action invoke)
        {
            Assert.That(() => invoke(), Throws
                .InstanceOf<AuthenticationException>()
                .And.Property("Message").EqualTo(_auphonicExceptionAuthenticationMessage)
                .And.Message.EqualTo(_auphonicExceptionAuthenticationMessage));
        }

        private void InvokeWithoutAuthenticationAsync(Func<Task> invoke)
        {
            Assert.That(async () => await invoke(), Throws
                .InstanceOf<AuthenticationException>()
                .And.Property("Message").EqualTo(_auphonicExceptionAuthenticationMessage)
                .And.Message.EqualTo(_auphonicExceptionAuthenticationMessage));
        }
        #endregion
    }
}
using AuphonicNet.Data;
using AuphonicNet.OAuth;
using AuphonicNet.Rest;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using static NUnit.Framework.TestContext;

namespace AuphonicNet.IntegrationTests
{
    [TestFixture]
    public class AuphonicNetIntegrationTestsWithAuthentication
    {
        #region Public Constants
#if DEBUG
        private const int timeout = int.MaxValue;
#else
        private const int timeout = 5000;
#endif
        #endregion

        #region Private Fields
        private string _clientId;
        private string _clientSecret;
        private string _username;
        private string _password;
        private string _accessToken;
        private string _productionFilePath;
        private string _logFilePath;
        private Auphonic _auphonic;

        private List<string> _executedTests = new List<string>();
        private List<string> _failedTests = new List<string>();

        private bool _hasClientCredentials;
        private bool _hasUserCredentials;
        private bool _hasAccessToken;

        private Preset _preset;
        private Production _production;
        private List<Service> _services;
        #endregion

        #region SetUp/TearDown
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _clientId = Environment.GetEnvironmentVariable("AUPHONIC_CLIENT_ID");
            _clientSecret = Environment.GetEnvironmentVariable("AUPHONIC_CLIENT_SECRET");
            _username = Environment.GetEnvironmentVariable("AUPHONIC_USERNAME");
            _password = Environment.GetEnvironmentVariable("AUPHONIC_PASSWORD");
            _accessToken = Environment.GetEnvironmentVariable("AUPHONIC_ACCESS_TOKEN");
            _productionFilePath = Environment.GetEnvironmentVariable("AUPHONIC_PRODUCTION_FILE_PATH");
            _logFilePath = Environment.GetEnvironmentVariable("AUPHONIC_LOG_FILE_PATH");

            _hasClientCredentials = !String.IsNullOrWhiteSpace(_clientId) && !String.IsNullOrWhiteSpace(_clientSecret);
            _hasUserCredentials = !String.IsNullOrWhiteSpace(_username) && !String.IsNullOrWhiteSpace(_password);
            _hasAccessToken = !String.IsNullOrWhiteSpace(_accessToken);

            if (_hasClientCredentials)
            {
                _auphonic = new Auphonic(_clientId, _clientSecret);

                if (_hasAccessToken)
                {
                    _auphonic.Authenticate(_accessToken);

                    if (!String.IsNullOrWhiteSpace(_logFilePath))
                    {
                        _auphonic.RecieveResponse += Auphonic_RecieveResponse;
                        _auphonic.SendRequest += Auphonic_SendRequest;
                    }
                }
            }
        }

        [TearDown]
        public void TearDown()
        {
            if (CurrentContext.Result.Outcome.Status == TestStatus.Passed)
            {
                _executedTests.Add(CurrentContext.Test.Name);
            }
            else if (CurrentContext.Result.Outcome.Status == TestStatus.Failed)
            {
                _failedTests.Add(CurrentContext.Test.Name);
            }
        }
        #endregion


        #region Tests - Authenticate
        [Test, Category("Authentication")]
        public void Invoke_Authenticate_With_Invalid_Client_Credentials_Throws_AuthenticationException()
        {
            HasUserCredentials();

            var auphonic = new Auphonic("clientId", "clientSecret");

            Assert.That(() => auphonic.Authenticate(_username, _password), Throws
                .InstanceOf<AuthenticationException>()
                .And.Property("Message").EqualTo("invalid_client: client_id clientId doesn't exist"));
        }

        [Test, Category("Authentication")]
        public void Invoke_Authenticate_With_Invalid_User_Credentials_Throws_AuthenticationException()
        {
            HasClientCredentials();

            var auphonic = new Auphonic(_clientId, _clientSecret);

            Assert.That(() => auphonic.Authenticate("username", "password"), Throws
                .InstanceOf<AuthenticationException>()
                .And.Property("Message").EqualTo("invalid_request: User authentication failed."));
        }

        [Test, Category("Authentication")]
        public void Invoke_Authenticate_With_Invalid_Token_Throws_AuthenticationException()
        {
            HasClientCredentials();

            var auphonic = new Auphonic(_clientId, _clientSecret);
            auphonic.Authenticate("24p5w92x9a");

            Assert.That(() => auphonic.GetAccountInfo(), Throws
                .InstanceOf<AuthenticationException>()
                .And.Property("Message").EqualTo("Token doesn't exist"));
        }

        [Test, Order(1), Timeout(timeout), Category("Authentication")]
        public void Invoke_Authenticate_Returns_Result()
        {
            HasUserCredentials();

            OAuthToken token = null;

            Assert.That(() => token = _auphonic.Authenticate(_username, _password), Throws.Nothing);
            Assert.That(token, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(String.IsNullOrWhiteSpace(token.AccessToken), Is.False, "AccessToken is null or whitespace");
                Assert.That(token.ExpiresIn, Is.EqualTo(315360000), "ExpiresIn");
                Assert.That(token.TokenType, Is.EqualTo("bearer"), "TokenType");
                Assert.That(String.IsNullOrWhiteSpace(token.Username), Is.False, "Username is null or whitespace");

                Assert.That(String.IsNullOrWhiteSpace(_auphonic.AccessToken), Is.False, "AccessToken is null or whitespace");
                Assert.That(_auphonic.AccessToken, Is.EqualTo(token.AccessToken), "AccessToken");

                _accessToken = token.AccessToken;
            });
        }
        #endregion

        #region Tests - GetAccountInfo
        [Test, Order(2), Timeout(timeout), Category("AccountInfo")]
        public void Invoke_GetAccountInfo_Returns_Result()
        {
            HasAccessToken();

            Account account = null;

            Assert.That(() => account = _auphonic.GetAccountInfo(), Throws.Nothing);
            Assert.That(account, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(account.DateJoined, Is.Not.EqualTo(DateTime.MinValue), "DateJoined");
                Assert.That(account.RechargeDate, Is.Not.EqualTo(DateTime.MinValue), "RechargeDate");
                Assert.That(String.IsNullOrWhiteSpace(account.Email), Is.False, "Email is null or whitespace");
                Assert.That(String.IsNullOrWhiteSpace(account.UserId), Is.False, "UserId is null or whitespace");
                Assert.That(String.IsNullOrWhiteSpace(account.Username), Is.False, "Username is null or whitespace");
            });
        }
        #endregion


        #region Tests - CreatePreset
        [Test, Order(3), Timeout(timeout), Category("Preset")]
        public void Invoke_CreatePreset_Returns_Result()
        {
            HasAccessToken();

            var presetName = "Integration Test";
            var preset = new Preset(presetName);

            Assert.That(() => _preset = _auphonic.CreatePreset(preset), Throws.Nothing);
            Assert.That(_preset, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(_preset.Algorithms, Is.Not.Null, "Algorithms");
                Assert.That(_preset.CreationTime, Is.Not.EqualTo(DateTime.MinValue), "CreationTime");
                Assert.That(_preset.Image, Is.Null, "Image");
                Assert.That(_preset.IsMultitrack, Is.False, "IsMultitrack");
                Assert.That(_preset.Metadata, Is.Not.Null, "Metadata");
                Assert.That(_preset.MultiInputFiles, Is.Not.Null, "MultiInputFiles");
                Assert.That(_preset.OutgoingServices, Is.Not.Null, "OutgoingServices");
                Assert.That(_preset.OutputBasename, Is.Null, "OutputBasename");
                Assert.That(_preset.OutputFiles, Is.Not.Null, "OutputFiles");
                Assert.That(_preset.PresetName, Is.EqualTo(presetName), "PresetName");
                Assert.That(_preset.SpeechRecognition, Is.Null, "SpeechRecognition");
                Assert.That(_preset.Thumbnail, Is.Null, "Thumbnail");
                Assert.That(String.IsNullOrWhiteSpace(_preset.Uuid), Is.False, "Uuid is null or whitespace");
                Assert.That(_preset.Webhook, Is.Null, "Webhook");
            });
        }
        #endregion

        #region Tests - UpdatePreset
        [Test, Order(4), Timeout(timeout), Category("Preset")]
        public void Invoke_UpdatePreset_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreatePreset_Returns_Result");

            _preset.PresetName += " Update";
            Preset preset = null;

            Assert.That(() => preset = _auphonic.UpdatePreset(_preset), Throws.Nothing);
            Assert.That(preset, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(preset.Uuid, Is.EqualTo(_preset.Uuid), "Uuid");
                Assert.That(preset.PresetName, Is.EqualTo(_preset.PresetName), "PresetName");
            });
        }
        #endregion

        #region Tests - GetPreset
        [Test, Order(5), Timeout(timeout), Category("Preset")]
        public void Invoke_GetPreset_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreatePreset_Returns_Result");

            Preset preset = null;

            Assert.That(() => preset = _auphonic.GetPreset(_preset.Uuid), Throws.Nothing);
            Assert.That(preset, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(preset.Uuid, Is.EqualTo(_preset.Uuid), "Uuid");
                Assert.That(preset.PresetName, Is.EqualTo(_preset.PresetName), "PresetName");
            });
        }
        #endregion

        #region Tests - GetPresets
        [Test, Order(6), Timeout(timeout), Category("Preset")]
        public void Invoke_GetPresets_Returns_Result()
        {
            HasAccessToken();

            List<Preset> presets = null;

            Assert.That(() => presets = _auphonic.GetPresets(), Throws.Nothing);
            Assert.That(presets, Is.Not.Null);

            if (!String.IsNullOrWhiteSpace(_preset?.Uuid))
            {
                Assert.That(presets.Any(o => o.Uuid == _preset.Uuid), Is.True);
            }
        }
        #endregion

        #region Tests - GetPresetsUuids
        [Test, Order(7), Timeout(timeout), Category("Preset")]
        public void Invoke_GetPresetsUuids_Returns_Result()
        {
            HasAccessToken();

            List<string> presetUuids = null;

            Assert.That(() => presetUuids = _auphonic.GetPresetsUuids(), Throws.Nothing);
            Assert.That(presetUuids, Is.Not.Null);

            if (!String.IsNullOrWhiteSpace(_preset?.Uuid))
            {
                Assert.That(presetUuids.Contains(_preset.Uuid), Is.True);
            }
        }
        #endregion


        #region Tests - CreateProduction
        [Test, Order(8), Timeout(timeout), Category("Production")]
        public void Invoke_CreateProduction_Returns_Result()
        {
            HasAccessToken();

            var production = new Production();

            if (!String.IsNullOrWhiteSpace(_preset?.Uuid))
            {
                production = new Production(_preset.Uuid);
            }

            Assert.That(() => _production = _auphonic.CreateProduction(production), Throws.Nothing);
            Assert.That(_production, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(_production.Algorithms, Is.Not.Null, "Algorithms");
                Assert.That(_production.Bitrate, Is.Null, "Bitrate");
                Assert.That(_production.ChangeAllowed, Is.True, "ChangeAllowed");
                Assert.That(_production.ChangeTime, Is.GreaterThan(DateTime.MinValue), "ChangeTime");
                Assert.That(_production.Channels, Is.Null, "Channels");
                Assert.That(_production.Chapters, Is.Not.Null, "Chapters");
                Assert.That(_production.CreationTime, Is.GreaterThan(DateTime.MinValue), "CreationTime");
                Assert.That(_production.CutEnd, Is.EqualTo(0), "CutEnd");
                Assert.That(_production.CutStart, Is.EqualTo(0), "CutStart");
                Assert.That(_production.EditPage, Is.EqualTo($"https://auphonic.com/engine/upload/edit/{_production.Uuid}"), "EditPage");
                Assert.That(_production.ErrorMessage, Is.EqualTo(String.Empty), "ErrorMessage");
                Assert.That(_production.ErrorStatus, Is.Null, "ErrorStatus");
                Assert.That(_production.Format, Is.EqualTo(String.Empty), "Format");
                Assert.That(_production.HasVideo, Is.False, "HasVideo");
                Assert.That(_production.Image, Is.Null, "Image");
                Assert.That(_production.InputFile, Is.Null, "InputFile");
                Assert.That(_production.IsMultitrack, Is.False, "IsMultitrack");
                Assert.That(_production.Length, Is.EqualTo(0), "Length");
                Assert.That(_production.LengthTimestring, Is.EqualTo(String.Empty), "LengthTimestring");
                Assert.That(_production.Metadata, Is.Not.Null, "Metadata");
                Assert.That(_production.MultiInputFiles, Is.Not.Null, "MultiInputFiles");
                Assert.That(_production.OutgoingServices, Is.Not.Null, "OutgoingServices");
                Assert.That(_production.OutputBasename, Is.Null, "OutputBasename");
                Assert.That(_production.OutputFiles, Is.Not.Null, "OutputFiles");
                Assert.That(_production.Samplerate, Is.Null, "Samplerate");
                Assert.That(_production.Service, Is.Null, "Service");
                Assert.That(_production.SpeechRecognition, Is.Null, "SpeechRecognition");
                Assert.That(_production.StartAllowed, Is.False, "StartAllowed");
                Assert.That(_production.Statistics, Is.Null, "Statistics");
                Assert.That(_production.Status, Is.EqualTo(ProductionStatus.IncompleteForm), "Status");
                Assert.That(_production.StatusPage, Is.EqualTo($"https://auphonic.com/engine/status/{_production.Uuid}"), "StatusPage");
                Assert.That(_production.StatusString, Is.EqualTo("Incomplete Form"), "StatusString");
                Assert.That(_production.Thumbnail, Is.Null, "Thumbnail");
                Assert.That(_production.UsedCredits, Is.Null, "UsedCredits");
                Assert.That(String.IsNullOrWhiteSpace(_production.Uuid), Is.False, "Uuid is null or whitespace");
                Assert.That(_production.WarningMessage, Is.EqualTo(String.Empty), "WarningMessage");
                Assert.That(_production.WarningStatus, Is.Null, "WarningStatus");
                Assert.That(_production.WaveformImage, Is.Null, "WaveformImage");
                Assert.That(_production.Webhook, Is.Null, "Webhook");
            });
        }
        #endregion

        #region Tests - SetProductionCoverImage
        [Test, Order(9), Timeout(timeout), Category("Production")]
        public void Invoke_SetProductionCoverImage_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");
            IsDependentOn("Invoke_CreatePreset_Returns_Result");

            Production production = null;

            Assert.That(() => production = _auphonic.SetProductionCoverImage(_production.Uuid, _preset.Uuid), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.Image, Is.Null, "Image");
        }
        #endregion

        #region Tests - DeleteProductionChapters
        [Test, Order(10), Timeout(timeout), Category("Production")]
        public void Invoke_DeleteProductionChapters_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");

            Production production = null;

            Assert.That(() => production = _auphonic.DeleteProductionChapters(_production.Uuid), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.Chapters.Count, Is.EqualTo(0), "Chapters");
        }
        #endregion

        #region Tests - DeleteProductionCoverImage
        [Test, Order(11), Timeout(timeout), Category("Production")]
        public void Invoke_DeleteProductionCoverImage_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");

            Production production = null;

            Assert.That(() => production = _auphonic.DeleteProductionCoverImage(_production.Uuid), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.Image, Is.Null, "Image");
        }
        #endregion

        #region Tests - DeleteProductionOutputFiles
        [Test, Order(12), Timeout(timeout), Category("Production")]
        public void Invoke_DeleteProductionOutputFiles_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");

            Production production = null;

            Assert.That(() => production = _auphonic.DeleteProductionOutputFiles(_production.Uuid), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.OutputFiles.Count, Is.EqualTo(0), "OutputFiles");
        }
        #endregion

        #region Tests - DeleteProductionSpeechRecognition
        [Test, Order(13), Timeout(timeout), Category("Production")]
        public void Invoke_DeleteProductionSpeechRecognition_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");

            Production production = null;

            Assert.That(() => production = _auphonic.DeleteProductionSpeechRecognition(_production.Uuid), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.SpeechRecognition, Is.Null, "SpeechRecognition");
        }
        #endregion

        #region Tests - UpdateProduction
        [Test, Order(14), Timeout(timeout), Category("Production")]
        public void Invoke_UpdateProduction_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");

            _production.OutputFiles = new List<OutputFile>
            {
                new OutputFile
                {
                    Suffix = "",
                    Format = "mp3",
                    Ending = "mp3",
                    SplitOnChapters = false,
                    Filename = "",
                    Bitrate = 112,
                    MonoMixdown = false,
                    OutgoingServices = new List<OutgoingService>()
                }
            };

            Production production = null;

            Assert.That(() => production = _auphonic.UpdateProduction(_production), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.Uuid, Is.EqualTo(_production.Uuid), "Uuid");
        }
        #endregion

        #region Tests - GetProduction
        [Test, Order(15), Timeout(timeout), Category("Production")]
        public void Invoke_GetProduction_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");

            Production production = null;

            Assert.That(() => production = _auphonic.GetProduction(_production.Uuid), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.Uuid, Is.EqualTo(_production.Uuid), "Uuid");
        }
        #endregion

        #region Tests - GetProductions
        [Test, Order(16), Timeout(timeout), Category("Production")]
        public void Invoke_GetProductions_Returns_Result()
        {
            HasAccessToken();

            List<Production> productions = null;

            Assert.That(() => productions = _auphonic.GetProductions(), Throws.Nothing);
            Assert.That(productions, Is.Not.Null);

            if (!String.IsNullOrWhiteSpace(_production?.Uuid))
            {
                Assert.That(productions.Any(o => o.Uuid == _production.Uuid), Is.True);
            }
        }
        #endregion

        #region Tests - GetProductionsUuids
        [Test, Order(17), Timeout(timeout), Category("Production")]
        public void Invoke_GetProductionsUuids_Returns_Result()
        {
            HasAccessToken();

            List<string> productionsUuids = null;

            Assert.That(() => productionsUuids = _auphonic.GetProductionsUuids(), Throws.Nothing);
            Assert.That(productionsUuids, Is.Not.Null);

            if (!String.IsNullOrWhiteSpace(_production?.Uuid))
            {
                Assert.That(productionsUuids.Contains(_production.Uuid), Is.True);
            }
        }
        #endregion

        #region Tests - UploadProductionFile
        [Test, Order(18), Timeout(timeout), Category("Production")]
        public void Invoke_UploadProductionFile_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");

            if (String.IsNullOrWhiteSpace(_productionFilePath))
            {
                Assert.Ignore("Unable to run Test, as necessary production file path is not available.");
            }

            Production production = null;

            Assert.That(() => production = _auphonic.UploadProductionFile(_production.Uuid, _productionFilePath), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.Status, Is.EqualTo(ProductionStatus.ProductionNotStartedYet), "Status");
        }
        #endregion

        #region Tests - StartProduction
        [Test, Order(19), Timeout(timeout), Category("Production")]
        public void Invoke_StartProduction_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_UploadProductionFile_Returns_Result");

            Production production = null;

            Assert.That(() => production = _auphonic.StartProduction(_production.Uuid), Throws.Nothing);
            Assert.That(production, Is.Not.Null);
            Assert.That(production.Status, Is.EqualTo(ProductionStatus.Waiting), "Status");
        }
        #endregion

        #region Tests - StopProduction
        [Test, Order(20), Timeout(timeout), Category("Production")]
        public void Invoke_StopProduction_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_StartProduction_Returns_Result");

            Assert.That(() => _auphonic.StopProduction(_production.Uuid), Throws.Nothing);
        }
        #endregion


        #region Tests - DeleteProduction
        [Test, Order(21), Timeout(timeout), Category("Production")]
        public void Invoke_DeleteProduction_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreateProduction_Returns_Result");

            Assert.That(() => _auphonic.DeleteProduction(_production.Uuid), Throws.Nothing);
        }
        #endregion

        #region Tests - DeletePreset
        [Test, Order(22), Timeout(timeout), Category("Preset")]
        public void Invoke_DeletePreset_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_CreatePreset_Returns_Result");

            Assert.That(() => _auphonic.DeletePreset(_preset.Uuid), Throws.Nothing);
        }
        #endregion


        #region Tests - GetServices
        [Test, Order(23), Timeout(timeout), Category("Service")]
        public void Invoke_GetServices_Returns_Result()
        {
            HasAccessToken();

            Assert.That(() => _services = _auphonic.GetServices(), Throws.Nothing);
            Assert.That(_services, Is.Not.Null);
        }
        #endregion

        #region Tests - GetServiceFiles
        [Test, Order(24), Timeout(timeout), Category("Service")]
        public void Invoke_GetServiceFiles_Returns_Result()
        {
            HasAccessToken();
            IsDependentOn("Invoke_GetServices_Returns_Result");

            if (_services == null || !_services.Any())
            {
                Assert.Inconclusive("Unable to run Test, as necessary services are not available.");
            }
            else
            {
                Service service = _services.First();
                List<string> files = null;

                Assert.That(() => files = _auphonic.GetServiceFiles(service.Uuid), Throws.Nothing);
            }
        }
        #endregion


        #region Private Methods
        private void IsDependentOn(string test)
        {
            if (_failedTests.Any(o => o == test))
            {
                Assert.Ignore($"Unable to run Test, as dependent Test '{test}' failed.");
            }
            else if (!_executedTests.Any(o => o == test))
            {
                Assert.Ignore($"Unable to run Test, as dependent Test '{test}' was not executed.");
            }
        }

        private void HasClientCredentials()
        {
            if (!_hasClientCredentials)
            {
                Assert.Inconclusive("Unable to run Test, as necessary Auphonic Client ID and Client Secret credentials are not available.");
            }
        }

        private void HasUserCredentials()
        {
            if (!_hasUserCredentials)
            {
                Assert.Inconclusive("Unable to run Test, as necessary Auphonic username and password credentials are not available.");
            }
        }

        private void HasAccessToken()
        {
            if (!_hasAccessToken)
            {
                Assert.Inconclusive("Unable to run Test, as necessary Auphonic OAuth access token is not available.");
            }
        }
        #endregion

        #region Private Event Handler
        private void Auphonic_SendRequest(object sender, RestRequestEventArgs e)
        {
            var sb = new StringBuilder()
                .AppendLine($"Method: {e.Request.Method}")
                .AppendLine($"Resource: {e.Request.Resource}")
                .AppendLine($"Parameters: {String.Join(Environment.NewLine, e.Request.Parameters)}")
                .AppendLine("----------------------------------------------------------------------------------------------------");

            File.AppendAllText(_logFilePath, sb.ToString());
        }

        private void Auphonic_RecieveResponse(object sender, RestResponseEventArgs e)
        {
            var sb = new StringBuilder()
                .AppendLine($"ContentType: {e.Response.ContentType}")
                .AppendLine($"ContentLength: {e.Response.ContentLength}")
                .AppendLine($"ContentEncoding: {e.Response.ContentEncoding}")
                .AppendLine($"Content: {e.Response.Content}")
                .AppendLine($"ResponseUri: {e.Response.ResponseUri}")
                .AppendLine($"ResponseStatus: {e.Response.ResponseStatus}")
                .AppendLine($"StatusCode: {e.Response.StatusCode}")
                .AppendLine($"Server: {e.Response.Server}")
                .AppendLine($"ErrorException: {e.Response.ErrorException}")
                .AppendLine($"ErrorMessage: {e.Response.ErrorMessage}")
                .AppendLine("----------------------------------------------------------------------------------------------------");

            File.AppendAllText(_logFilePath, sb.ToString());
        }
        #endregion
    }
}
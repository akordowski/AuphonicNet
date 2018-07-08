using AuphonicNet.Data;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;

namespace AuphonicNet.IntegrationTests
{
    [TestFixture]
    public class AuphonicNetIntegrationTestsWithoutAuthentication
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
        private Auphonic _auphonic;

        private bool _hasClientCredentials;
        #endregion

        #region SetUp/TearDown
        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            _clientId = Environment.GetEnvironmentVariable("AUPHONIC_CLIENT_ID");
            _clientSecret = Environment.GetEnvironmentVariable("AUPHONIC_CLIENT_SECRET");

            _hasClientCredentials = !String.IsNullOrWhiteSpace(_clientId) && !String.IsNullOrWhiteSpace(_clientSecret);

            if (_hasClientCredentials)
            {
                _auphonic = new Auphonic(_clientId, _clientSecret);
            }
        }
        #endregion

        #region Tests
        [Test, Order(1), Timeout(timeout), Category("Info")]
        public void Invoke_GetAlgorithms_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, Algorithm> result = null;

            Assert.That(() => result = _auphonic.GetAlgorithms(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(2), Timeout(timeout), Category("Info")]
        public void Invoke_GetAlgorithmsAsync_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, Algorithm> result = null;

            Assert.That(async () => result = await _auphonic.GetAlgorithmsAsync(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(3), Timeout(timeout), Category("Info")]
        public void Invoke_GetAlgorithmsAsync_With_CancellationToken_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, Algorithm> result = null;

            Assert.That(async () => result = await _auphonic.GetAlgorithmsAsync(CancellationToken.None), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(4), Timeout(timeout), Category("Info")]
        public void Invoke_GetFileEndings_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, List<string>> result = null;

            Assert.That(() => result = _auphonic.GetFileEndings(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(5), Timeout(timeout), Category("Info")]
        public void Invoke_GetFileEndingsAsync_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, List<string>> result = null;

            Assert.That(async () => result = await _auphonic.GetFileEndingsAsync(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(6), Timeout(timeout), Category("Info")]
        public void Invoke_GetFileEndingsAsync_With_CancellationToken_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, List<string>> result = null;

            Assert.That(async () => result = await _auphonic.GetFileEndingsAsync(CancellationToken.None), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(7), Timeout(timeout), Category("Info")]
        public void InvokeGetOutputFileTypes_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, OutputFileType> result = null;

            Assert.That(() => result = _auphonic.GetOutputFileTypes(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(8), Timeout(timeout), Category("Info")]
        public void Invoke_GetOutputFileTypesAsync_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, OutputFileType> result = null;

            Assert.That(async () => result = await _auphonic.GetOutputFileTypesAsync(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(9), Timeout(timeout), Category("Info")]
        public void Invoke_GetOutputFileTypesAsync_With_CancellationToken_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, OutputFileType> result = null;

            Assert.That(async () => result = await _auphonic.GetOutputFileTypesAsync(CancellationToken.None), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(10), Timeout(timeout), Category("Info")]
        public void Invoke_GetProductionStatus_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<ProductionStatus, string> result = null;

            Assert.That(() => result = _auphonic.GetProductionStatus(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(15));
        }

        [Test, Order(11), Timeout(timeout), Category("Info")]
        public void Invoke_GetProductionStatusAsync_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<ProductionStatus, string> result = null;

            Assert.That(async () => result = await _auphonic.GetProductionStatusAsync(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(15));
        }

        [Test, Order(12), Timeout(timeout), Category("Info")]
        public void Invoke_GetProductionStatusAsync_With_CancellationToken_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<ProductionStatus, string> result = null;

            Assert.That(async () => result = await _auphonic.GetProductionStatusAsync(CancellationToken.None), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(15));
        }

        [Test, Order(13), Timeout(timeout), Category("Info")]
        public void Invoke_GetServiceTypes_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, ServiceType> result = null;

            Assert.That(() => result = _auphonic.GetServiceTypes(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(14), Timeout(timeout), Category("Info")]
        public void Invoke_GetServiceTypesAsync_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, ServiceType> result = null;

            Assert.That(async () => result = await _auphonic.GetServiceTypesAsync(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(15), Timeout(timeout), Category("Info")]
        public void Invoke_GetServiceTypesAsync_With_CancellationToken_Returns_Result()
        {
            HasClientCredentials();

            Dictionary<string, ServiceType> result = null;

            Assert.That(async () => result = await _auphonic.GetServiceTypesAsync(CancellationToken.None), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.GreaterThan(0));
        }

        [Test, Order(16), Timeout(timeout), Category("Info")]
        public void Invoke_GetInfo_Returns_Result()
        {
            HasClientCredentials();

            Info result = null;

            Assert.That(() => result = _auphonic.GetInfo(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
        }

        [Test, Order(17), Timeout(timeout), Category("Info")]
        public void Invoke_GetInfoAsync_Returns_Result()
        {
            HasClientCredentials();

            Info result = null;

            Assert.That(async () => result = await _auphonic.GetInfoAsync(), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
        }

        [Test, Order(18), Timeout(timeout), Category("Info")]
        public void Invoke_GetInfoAsync_With_CancellationToken_Returns_Result()
        {
            HasClientCredentials();

            Info result = null;

            Assert.That(async () => result = await _auphonic.GetInfoAsync(CancellationToken.None), Throws.Nothing);
            Assert.That(result, Is.Not.Null);
        }
        #endregion

        #region Private Methods
        private void HasClientCredentials()
        {
            if (!_hasClientCredentials)
            {
                Assert.Inconclusive("Unable to run Test, as necessary Auphonic Client ID and Client Secret credentials are not available.");
            }
        }
        #endregion
    }
}
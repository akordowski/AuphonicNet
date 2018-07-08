using AuphonicNet.Data;
using AuphonicNet.Json;
using AuphonicNet.OAuth;
using AuphonicNet.Rest;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Security.Authentication;
using System.Threading;
using System.Threading.Tasks;

namespace AuphonicNet
{
    /// <summary>
    /// Provides a class for the Auphonic API.
    /// </summary>
    public class Auphonic
    {
        #region Public Events
        /// <summary>
        /// Occurs when the response has been received.
        /// </summary>
        public event EventHandler<RestResponseEventArgs> RecieveResponse;

        /// <summary>
        /// Occurs when the request has been send.
        /// </summary>
        public event EventHandler<RestRequestEventArgs> SendRequest;
        #endregion

        #region Public Properties
        /// <summary>
        /// Gets the OAuth access token.
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Gets the Auphonic base URL.
        /// </summary>
        public string BaseUrl { get; }

        /// <summary>
        /// Gets the Auphonic Client ID.
        /// </summary>
        public string ClientId { get; }

        /// <summary>
        /// Gets the Auphonic Client Secret.
        /// </summary>
        public string ClientSecret { get; }
        #endregion

        #region Private Fields
        private readonly IRestClient _client;
        #endregion

        #region Private Enums
        private enum AuthenticatorType
        {
            None,
            Basic,
            OAuth2
        }

        private enum RequestType
        {
            None,
            Json,
        }
        #endregion

        #region Constructor
        /// <summary>
        /// Initializes a new instance of the <see cref="Auphonic"/> class.
        /// </summary>
        /// <param name="clientId">The Auphonic Client ID.</param>
        /// <param name="clientSecret">The Auphonic Client Secret.</param>
        /// <exception cref="ArgumentException"><em>clientId</em> or <em>clientSecret</em> is
        /// empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>clientId</em> or <em>clientSecret</em> is
        /// <strong>null</strong>.</exception>
        public Auphonic(string clientId, string clientSecret)
        {
            Precondition.IsNotNullOrWhiteSpace(clientId, nameof(clientId));
            Precondition.IsNotNullOrWhiteSpace(clientSecret, nameof(clientSecret));

            BaseUrl = "https://auphonic.com";
            ClientId = clientId;
            ClientSecret = clientSecret;

            _client = new RestClient(BaseUrl);
            _client.AddHandler("application/json", NewtonsoftJsonSerializer.Default);
            _client.AddHandler("text/json", NewtonsoftJsonSerializer.Default);
            _client.AddHandler("text/x-json", NewtonsoftJsonSerializer.Default);
            _client.AddHandler("text/javascript", NewtonsoftJsonSerializer.Default);
            _client.AddHandler("*+json", NewtonsoftJsonSerializer.Default);
        }
        #endregion


        #region Authenticate
        /// <summary>
        /// Sets the OAuth access token.
        /// </summary>
        /// <param name="accessToken">The OAuth access token.</param>
        /// <exception cref="ArgumentException"><em>accessToken</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>accessToken</em> is <strong>null</strong>.</exception>
        public void Authenticate(string accessToken)
        {
            Precondition.IsNotNullOrWhiteSpace(accessToken, nameof(accessToken));

            AccessToken = accessToken;
        }

        /// <summary>
        /// Authenticates a user on the Auphonic API.
        /// </summary>
        /// <param name="username">The Auphonic username.</param>
        /// <param name="password">The Auphonic password.</param>
        /// <returns>A OAuth Token for the authenticated user.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>username</em> or <em>password</em> is an empty
        /// string or contains whitespace characters.</exception>
        /// <exception cref="ArgumentNullException"><em>password</em> or <em>password</em> is
        /// <strong>null</strong>.</exception>
        public OAuthToken Authenticate(string username, string password)
        {
            return Task.Run(async () => await AuthenticateAsync(username, password)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Authenticates a user on the Auphonic API asynchronously.
        /// </summary>
        /// <param name="username">The Auphonic username.</param>
        /// <param name="password"></param>
        /// <returns></returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>username</em> or <em>password</em> is an empty
        /// string or contains whitespace characters.</exception>
        /// <exception cref="ArgumentNullException"><em>password</em> or <em>password</em> is
        /// <strong>null</strong>.</exception>
        public async Task<OAuthToken> AuthenticateAsync(string username, string password)
        {
            return await AuthenticateAsync(username, password, CancellationToken.None);
        }

        /// <summary>
        /// Authenticates a user on the Auphonic API asynchronously.
        /// </summary>
        /// <param name="username">The Auphonic username.</param>
        /// <param name="password">The Auphonic password.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the <em>TResult</em> parameter contains the ...</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>username</em> or <em>password</em> is an empty
        /// string or contains whitespace characters.</exception>
        /// <exception cref="ArgumentNullException"><em>password</em> or <em>password</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<OAuthToken> AuthenticateAsync(string username, string password, CancellationToken cancellationToken)
        {
            Precondition.IsNotNullOrWhiteSpace(username, nameof(username));
            Precondition.IsNotNullOrWhiteSpace(password, nameof(password));

            var request = new Rest.RestRequest("oauth2/token/", Method.POST);
            request.AddParameter("client_id", ClientId);
            request.AddParameter("username", username);
            request.AddParameter("password", password);
            request.AddParameter("grant_type", "password");

            var response = await ExecuteRequestAsync<OAuthToken>(cancellationToken, request, AuthenticatorType.Basic, RequestType.None);
            AccessToken = response.AccessToken;

            return response;
        }
        #endregion

        #region GetAccountInfo
        /// <summary>
        /// Gets the user account info.
        /// </summary>
        /// <returns>The user account info.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Account GetAccountInfo()
        {
            return Task.Run(async () => await GetAccountInfoAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets the user account info asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the user account info.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Account> GetAccountInfoAsync()
        {
            return await GetAccountInfoAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets the user account info asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the user account info.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Account> GetAccountInfoAsync(CancellationToken cancellationToken)
        {
            CheckAuthentication();

            var request = new Rest.RestRequest("api/user.json");
            var response = await ExecuteRequestAsync<Response<Account>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion


        #region CreatePreset
        /// <summary>
        /// Creates a new preset.
        /// </summary>
        /// <param name="preset">The preset to create.</param>
        /// <returns>The created preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>preset</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Preset CreatePreset(Preset preset)
        {
            return Task.Run(async () => await CreatePresetAsync(preset)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Creates a new preset asynchronously.
        /// </summary>
        /// <param name="preset">The preset to create.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the <em>TResult</em>
        /// parameter contains the created preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>preset</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Preset> CreatePresetAsync(Preset preset)
        {
            return await CreatePresetAsync(preset, CancellationToken.None);
        }

        /// <summary>
        /// Creates a new preset asynchronously.
        /// </summary>
        /// <param name="preset">The preset to create.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the <em>TResult</em>
        /// parameter contains the created preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>preset</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Preset> CreatePresetAsync(Preset preset, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNull(preset, nameof(preset));

            var request = new Rest.RestRequest("api/presets.json", Method.POST);
            request.AddJsonBody(preset);

            var response = await ExecuteRequestAsync<Response<Preset>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region DeletePreset
        /// <summary>
        /// Deletes a preset.
        /// </summary>
        /// <param name="presetUuid">The UUID of the preset to delete.</param>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>presetUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>presetUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public void DeletePreset(string presetUuid)
        {
            Task.Run(async () => await DeletePresetAsync(presetUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deletes a preset asynchronously.
        /// </summary>
        /// <param name="presetUuid">The UUID of the preset to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>presetUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>presetUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task DeletePresetAsync(string presetUuid)
        {
            await DeletePresetAsync(presetUuid, CancellationToken.None);
        }

        /// <summary>
        /// Deletes a preset asynchronously.
        /// </summary>
        /// <param name="presetUuid">The UUID of the preset to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>presetUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>presetUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task DeletePresetAsync(string presetUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(presetUuid, nameof(presetUuid));

            var request = new Rest.RestRequest("api/preset/{presetUuid}.json", Method.DELETE);
            request.AddUrlSegment("presetUuid", presetUuid);

            await ExecuteRequestAsync<Response<object>>(cancellationToken, request, AuthenticatorType.OAuth2);
        }
        #endregion

        #region GetPreset
        /// <summary>
        /// Gets a preset.
        /// </summary>
        /// <param name="presetUuid">The preset UUID.</param>
        /// <returns>The preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>presetUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>presetUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Preset GetPreset(string presetUuid)
        {
            return Task.Run(async () => await GetPresetAsync(presetUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a preset asynchronously.
        /// </summary>
        /// <param name="presetUuid">The preset UUID.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>presetUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>presetUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Preset> GetPresetAsync(string presetUuid)
        {
            return await GetPresetAsync(presetUuid, CancellationToken.None);
        }

        /// <summary>
        /// Gets a preset asynchronously.
        /// </summary>
        /// <param name="presetUuid">The preset UUID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>presetUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>presetUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Preset> GetPresetAsync(string presetUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(presetUuid, nameof(presetUuid));

            var request = new Rest.RestRequest("api/preset/{presetUuid}.json");
            request.AddUrlSegment("presetUuid", presetUuid);

            var response = await ExecuteRequestAsync<Response<Preset>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region GetPresets
        /// <summary>
        /// Gets a list of user presets.
        /// </summary>
        /// <returns>A list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<Preset> GetPresets()
        {
            return Task.Run(async () => await GetPresetsAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list of user presets.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <returns>A list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<Preset> GetPresets(int limit)
        {
            return Task.Run(async () => await GetPresetsAsync(limit)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list of user presets.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <param name="offset">List starts at the defined number.</param>
        /// <returns>A list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<Preset> GetPresets(int limit, int offset)
        {
            return Task.Run(async () => await GetPresetsAsync(limit, offset)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list of user presets asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Preset>> GetPresetsAsync()
        {
            return await GetPresetsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets a list of user presets asynchronously.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Preset>> GetPresetsAsync(int limit)
        {
            return await GetPresetsAsync(limit, CancellationToken.None);
        }

        /// <summary>
        /// Gets a list of user presets asynchronously.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <param name="offset">List starts at the defined number.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Preset>> GetPresetsAsync(int limit, int offset)
        {
            return await GetPresetsAsync(limit, offset, CancellationToken.None);
        }

        /// <summary>
        /// Gets a list of user presets asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Preset>> GetPresetsAsync(CancellationToken cancellationToken)
        {
            return await GetPresetsAsync(0, 0, cancellationToken);
        }

        /// <summary>
        /// Gets a list of user presets asynchronously.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Preset>> GetPresetsAsync(int limit, CancellationToken cancellationToken)
        {
            return await GetPresetsAsync(limit, 0, cancellationToken);
        }

        /// <summary>
        /// Gets a list of user presets asynchronously.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <param name="offset">List starts at the defined number.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user presets.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Preset>> GetPresetsAsync(int limit, int offset, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsGreaterOrEqual(limit, 0, nameof(limit));
            Precondition.IsGreaterOrEqual(offset, 0, nameof(offset));

            var request = new Rest.RestRequest("api/presets.json");

            if (limit > 0)
            {
                request.AddParameter("limit", limit);
            }

            if (offset > 0)
            {
                request.AddParameter("offset", offset);
            }

            var response = await ExecuteRequestAsync<Response<List<Preset>>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region GetPresetsUuids
        /// <summary>
        /// Gets a list with all preset UUIDs.
        /// </summary>
        /// <returns>The list with all preset UUIDs.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<string> GetPresetsUuids()
        {
            return Task.Run(async () => await GetPresetsUuidsAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list with all preset UUIDs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the list with all preset UUIDs.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<string>> GetPresetsUuidsAsync()
        {
            return await GetPresetsUuidsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets a list with all preset UUIDs asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the list with all preset UUIDs.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<string>> GetPresetsUuidsAsync(CancellationToken cancellationToken)
        {
            CheckAuthentication();

            var request = new Rest.RestRequest("api/presets.json");
            request.AddParameter("uuids_only", 1);

            var response = await ExecuteRequestAsync<Response<List<string>>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region UpdatePreset
        /// <summary>
        /// Updates a preset.
        /// </summary>
        /// <param name="preset">The preset to update.</param>
        /// <returns>The updated preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>preset</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Preset UpdatePreset(Preset preset)
        {
            return Task.Run(async () => await UpdatePresetAsync(preset)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Updates a preset asynchronously.
        /// </summary>
        /// <param name="preset">The preset to update.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>preset</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Preset> UpdatePresetAsync(Preset preset)
        {
            return await UpdatePresetAsync(preset, CancellationToken.None);
        }

        /// <summary>
        /// Updates a preset asynchronously.
        /// </summary>
        /// <param name="preset">The preset to update.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated preset.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>preset</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Preset> UpdatePresetAsync(Preset preset, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNull(preset, nameof(preset));
            Precondition.IsValid(() => String.IsNullOrWhiteSpace(preset.Uuid), "Preset UUID cannot be null or empty.", nameof(preset));

            var request = new Rest.RestRequest("api/preset/{uuid}.json", Method.POST);
            request.AddUrlSegment("uuid", preset.Uuid);
            request.AddJsonBody(preset);

            var response = await ExecuteRequestAsync<Response<Preset>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion


        #region CreateProduction
        /// <summary>
        /// Creates a new production.
        /// </summary>
        /// <param name="production">The production to create.</param>
        /// <returns>The created production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>production</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production CreateProduction(Production production)
        {
            return Task.Run(async () => await CreateProductionAsync(production)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Creates a new production asynchronously.
        /// </summary>
        /// <param name="production">The production to create.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the <em>TResult</em>
        /// parameter contains the created production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>production</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> CreateProductionAsync(Production production)
        {
            return await CreateProductionAsync(production, CancellationToken.None);
        }

        /// <summary>
        /// Creates a new production asynchronously.
        /// </summary>
        /// <param name="production">The production to create.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the <em>TResult</em>
        /// parameter contains the created production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>production</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> CreateProductionAsync(Production production, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNull(production, nameof(production));

            var request = new Rest.RestRequest("api/productions.json", Method.POST);
            request.AddJsonBody(production);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region DeleteProduction
        /// <summary>
        /// Deletes a production.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete.</param>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public void DeleteProduction(string productionUuid)
        {
            Task.Run(async () => await DeleteProductionAsync(productionUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deletes a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task DeleteProductionAsync(string productionUuid)
        {
            await DeleteProductionAsync(productionUuid, CancellationToken.None);
        }

        /// <summary>
        /// Deletes a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous delete operation.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task DeleteProductionAsync(string productionUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}.json", Method.DELETE);
            request.AddUrlSegment("productionUuid", productionUuid);

            await ExecuteRequestAsync<Response<object>>(cancellationToken, request, AuthenticatorType.OAuth2);
        }
        #endregion

        #region DeleteProductionChapters
        /// <summary>
        /// Deletes the chapters for a production.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the chapters.</param>
        /// <returns>The updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production DeleteProductionChapters(string productionUuid)
        {
            return Task.Run(async () => await DeleteProductionChaptersAsync(productionUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deletes the chapters for a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the chapters.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> DeleteProductionChaptersAsync(string productionUuid)
        {
            return await DeleteProductionChaptersAsync(productionUuid, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the chapters for a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the chapters.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> DeleteProductionChaptersAsync(string productionUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}/chapters.json", Method.DELETE);
            request.AddUrlSegment("productionUuid", productionUuid);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region DeleteProductionCoverImage
        /// <summary>
        /// Deletes the cover image for a production.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the cover image.</param>
        /// <returns>The updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production DeleteProductionCoverImage(string productionUuid)
        {
            return Task.Run(async () => await DeleteProductionCoverImageAsync(productionUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deletes the cover image for a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the cover image.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> DeleteProductionCoverImageAsync(string productionUuid)
        {
            return await DeleteProductionCoverImageAsync(productionUuid, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the cover image for a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the cover image.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> DeleteProductionCoverImageAsync(string productionUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}.json", Method.POST);
            request.AddUrlSegment("productionUuid", productionUuid);
            request.AddParameter("application/json; charset=utf-8", "{\"reset_cover_image\": true}", ParameterType.RequestBody);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region DeleteProductionOutputFiles
        /// <summary>
        /// Deletes the output files for a production.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the output files.</param>
        /// <returns>The updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production DeleteProductionOutputFiles(string productionUuid)
        {
            return Task.Run(async () => await DeleteProductionOutputFilesAsync(productionUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deletes the output files for a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the output files.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> DeleteProductionOutputFilesAsync(string productionUuid)
        {
            return await DeleteProductionOutputFilesAsync(productionUuid, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the output files for a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the output files.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> DeleteProductionOutputFilesAsync(string productionUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}/output_files.json", Method.DELETE);
            request.AddUrlSegment("productionUuid", productionUuid);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region DeleteProductionSpeechRecognition
        /// <summary>
        /// Deletes the speech recognition for a production.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the speech recognition.</param>
        /// <returns>The updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production DeleteProductionSpeechRecognition(string productionUuid)
        {
            return Task.Run(async () => await DeleteProductionSpeechRecognitionAsync(productionUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Deletes the speech recognition for a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the speech recognition.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> DeleteProductionSpeechRecognitionAsync(string productionUuid)
        {
            return await DeleteProductionSpeechRecognitionAsync(productionUuid, CancellationToken.None);
        }

        /// <summary>
        /// Deletes the speech recognition for a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to delete the speech recognition.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> DeleteProductionSpeechRecognitionAsync(string productionUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}/speech_recognition.json", Method.DELETE);
            request.AddUrlSegment("productionUuid", productionUuid);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region GetProduction
        /// <summary>
        /// Gets a production.
        /// </summary>
        /// <param name="productionUuid">The production UUID.</param>
        /// <returns>The production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production GetProduction(string productionUuid)
        {
            return Task.Run(async () => await GetProductionAsync(productionUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The production UUID.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> GetProductionAsync(string productionUuid)
        {
            return await GetProductionAsync(productionUuid, CancellationToken.None);
        }

        /// <summary>
        /// Gets a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The production UUID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> GetProductionAsync(string productionUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}.json");
            request.AddUrlSegment("productionUuid", productionUuid);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region GetProductions
        /// <summary>
        /// Gets a list of user productions.
        /// </summary>
        /// <returns>A list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<Production> GetProductions()
        {
            return Task.Run(async () => await GetProductionsAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list of user productions.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <returns>A list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<Production> GetProductions(int limit)
        {
            return Task.Run(async () => await GetProductionsAsync(limit)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list of user productions.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <param name="offset">List starts at the defined number.</param>
        /// <returns>A list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<Production> GetProductions(int limit, int offset)
        {
            return Task.Run(async () => await GetProductionsAsync(limit, offset)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list of user productions asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Production>> GetProductionsAsync()
        {
            return await GetProductionsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets a list of user productions asynchronously.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Production>> GetProductionsAsync(int limit)
        {
            return await GetProductionsAsync(limit, CancellationToken.None);
        }

        /// <summary>
        /// Gets a list of user productions asynchronously.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <param name="offset">List starts at the defined number.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Production>> GetProductionsAsync(int limit, int offset)
        {
            return await GetProductionsAsync(limit, offset, CancellationToken.None);
        }

        /// <summary>
        /// Gets a list of user productions asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Production>> GetProductionsAsync(CancellationToken cancellationToken)
        {
            return await GetProductionsAsync(0, 0, cancellationToken);
        }

        /// <summary>
        /// Gets a list of user productions asynchronously.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Production>> GetProductionsAsync(int limit, CancellationToken cancellationToken)
        {
            return await GetProductionsAsync(limit, 0, cancellationToken);
        }

        /// <summary>
        /// Gets a list of user productions asynchronously.
        /// </summary>
        /// <param name="limit">Limits the number of returned entries.</param>
        /// <param name="offset">List starts at the defined number.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list of user productions.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentOutOfRangeException"><em>limit</em> or <em>offset</em> is
        /// less the 0.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Production>> GetProductionsAsync(int limit, int offset, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsGreaterOrEqual(limit, 0, nameof(limit));
            Precondition.IsGreaterOrEqual(offset, 0, nameof(offset));

            IRestRequest request = new Rest.RestRequest("api/productions.json");

            if (limit > 0)
            {
                request.AddParameter("limit", limit);
            }

            if (offset > 0)
            {
                request.AddParameter("offset", offset);
            }

            var response = await ExecuteRequestAsync<Response<List<Production>>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region GetProductionsUuids
        /// <summary>
        /// Gets a list with all production UUIDs.
        /// </summary>
        /// <returns>The list with all production UUIDs.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<string> GetProductionsUuids()
        {
            return Task.Run(async () => await GetProductionsUuidsAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list with all production UUIDs asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the list with all production UUIDs.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<string>> GetProductionsUuidsAsync()
        {
            return await GetProductionsUuidsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets a list with all production UUIDs asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the list with all production UUIDs.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<string>> GetProductionsUuidsAsync(CancellationToken cancellationToken)
        {
            CheckAuthentication();

            var request = new Rest.RestRequest("api/productions.json");
            request.AddParameter("uuids_only", 1);

            var response = await ExecuteRequestAsync<Response<List<string>>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region SetProductionCoverImage
        /// <summary>
        /// Sets a cover image for a production from a preset.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to set the cover image.</param>
        /// <param name="presetUuid">The UUID of the preset to get the cover image from.</param>
        /// <returns>he updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> or <em>presetUuid</em> is
        /// empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> or <em>presetUuid</em>
        /// is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production SetProductionCoverImage(string productionUuid, string presetUuid)
        {
            return Task.Run(async () => await SetProductionCoverImageFromPresetAsync(productionUuid, presetUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Sets a cover image for a production from a preset asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to set the cover image.</param>
        /// <param name="presetUuid">The UUID of the preset to get the cover image from.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> or <em>presetUuid</em> is
        /// empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> or <em>presetUuid</em>
        /// is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> SetProductionCoverImageFromPresetAsync(string productionUuid, string presetUuid)
        {
            return await SetProductionCoverImageFromPresetAsync(productionUuid, presetUuid, CancellationToken.None);
        }

        /// <summary>
        /// Sets a cover image for a production from a preset asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to set the cover image.</param>
        /// <param name="presetUuid">The UUID of the preset to get the cover image from.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> or <em>presetUuid</em> is
        /// empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> or <em>presetUuid</em>
        /// is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> SetProductionCoverImageFromPresetAsync(string productionUuid, string presetUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));
            Precondition.IsNotNullOrWhiteSpace(presetUuid, nameof(presetUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}.json");
            request.AddUrlSegment("productionUuid", productionUuid);
            request.AddParameter("application/json; charset=utf-8", "{\"preset_cover_image\": " + presetUuid + "}", ParameterType.RequestBody);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region StartProduction
        /// <summary>
        /// Starts a production.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to start.</param>
        /// <returns>The updated production</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production StartProduction(string productionUuid)
        {
            return Task.Run(async () => await StartProductionAsync(productionUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Starts a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to start.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> StartProductionAsync(string productionUuid)
        {
            return await StartProductionAsync(productionUuid, CancellationToken.None);
        }

        /// <summary>
        /// Starts a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to start.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> StartProductionAsync(string productionUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}/start.json", Method.POST);
            request.AddUrlSegment("productionUuid", productionUuid);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region StopProduction
        /// <summary>
        /// Stops a production.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to stop.</param>
        /// <returns>The updated production</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public void StopProduction(string productionUuid)
        {
            Task.Run(async () => await StopProductionAsync(productionUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Stops a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to stop.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task StopProductionAsync(string productionUuid)
        {
            await StopProductionAsync(productionUuid, CancellationToken.None);
        }

        /// <summary>
        /// Stops a production asynchronously.
        /// </summary>
        /// <param name="productionUuid">The UUID of the production to stop.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task StopProductionAsync(string productionUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));

            var request = new Rest.RestRequest("api/production/{productionUuid}/stop.json", Method.POST);
            request.AddUrlSegment("productionUuid", productionUuid);

            await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);
        }
        #endregion

        #region UpdateProduction
        /// <summary>
        /// Updates a production.
        /// </summary>
        /// <param name="production">The production to update.</param>
        /// <returns>The updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>production</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production UpdateProduction(Production production)
        {
            return Task.Run(async () => await UpdateProductionAsync(production)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Updates a production asynchronously.
        /// </summary>
        /// <param name="production">The production to update.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>production</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> UpdateProductionAsync(Production production)
        {
            return await UpdateProductionAsync(production, CancellationToken.None);
        }

        /// <summary>
        /// Updates a production asynchronously.
        /// </summary>
        /// <param name="production">The production to update.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentNullException"><em>production</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> UpdateProductionAsync(Production production, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNull(production, nameof(production));
            Precondition.IsValid(() => String.IsNullOrWhiteSpace(production.Uuid), "Production UUID cannot be null or empty.", nameof(production));

            var request = new Rest.RestRequest("api/production/{uuid}.json", Method.POST);
            request.AddUrlSegment("uuid", production.Uuid);
            request.AddJsonBody(production);

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region UploadProductionFile
        /// <summary>
        /// Uploads a production file.
        /// </summary>
        /// <param name="productionUuid">The production UUID to upload the file for.</param>
        /// <param name="filePath">The path to the file to upload.</param>
        /// <returns>The updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> or <em>filePath</em> is
        /// empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> or <em>filePath</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="FileNotFoundException"><em>filePath</em> don't exists.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Production UploadProductionFile(string productionUuid, string filePath)
        {
            return Task.Run(async () => await UploadProductionFileAsync(productionUuid, filePath)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Uploads a production file asynchronously.
        /// </summary>
        /// <param name="productionUuid">The production UUID to upload the file for.</param>
        /// <param name="filePath">The path to the file to upload.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> or <em>filePath</em> is
        /// empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> or <em>filePath</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="FileNotFoundException"><em>filePath</em> don't exists.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> UploadProductionFileAsync(string productionUuid, string filePath)
        {
            return await UploadProductionFileAsync(productionUuid, filePath, CancellationToken.None);
        }

        /// <summary>
        /// Uploads a production file asynchronously.
        /// </summary>
        /// <param name="productionUuid">The production UUID to upload the file for.</param>
        /// <param name="filePath">The path to the file to upload.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the updated production.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>productionUuid</em> or <em>filePath</em> is
        /// empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>productionUuid</em> or <em>filePath</em> is
        /// <strong>null</strong>.</exception>
        /// <exception cref="FileNotFoundException"><em>filePath</em> don't exists.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Production> UploadProductionFileAsync(string productionUuid, string filePath, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(productionUuid, nameof(productionUuid));
            Precondition.IsNotNullOrWhiteSpace(filePath, nameof(filePath));
            Precondition.FileExists(filePath, nameof(filePath));

            var request = new Rest.RestRequest("api/production/{productionUuid}/upload.json", Method.POST);
            request.AddUrlSegment("productionUuid", productionUuid);
            request.AddFile("input_file", File.ReadAllBytes(filePath), Path.GetFileName(filePath), "multipart/form-data");

            var response = await ExecuteRequestAsync<Response<Production>>(cancellationToken, request, AuthenticatorType.OAuth2, RequestType.None);

            return response.Data;
        }
        #endregion


        #region GetServiceFiles
        /// <summary>
        /// Gets a list with files available on a service.
        /// </summary>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <returns>A list with files available on a service.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>serviceUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>serviceUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<string> GetServiceFiles(string serviceUuid)
        {
            return Task.Run(async () => await GetServiceFilesAsync(serviceUuid)).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list with files available on a service asynchronously.
        /// </summary>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list with files available on a service.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>serviceUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>serviceUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<string>> GetServiceFilesAsync(string serviceUuid)
        {
            return await GetServiceFilesAsync(serviceUuid, CancellationToken.None);
        }

        /// <summary>
        /// Gets a list with files available on a service asynchronously.
        /// </summary>
        /// <param name="serviceUuid">The service UUID.</param>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains a list with files available on a service.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="ArgumentException"><em>serviceUuid</em> is empty or whitespace.</exception>
        /// <exception cref="ArgumentNullException"><em>serviceUuid</em> is <strong>null</strong>.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<string>> GetServiceFilesAsync(string serviceUuid, CancellationToken cancellationToken)
        {
            CheckAuthentication();
            Precondition.IsNotNullOrWhiteSpace(serviceUuid, nameof(serviceUuid));

            var request = new Rest.RestRequest("api/service/{serviceUuid}/ls.json");
            request.AddUrlSegment("serviceUuid", serviceUuid);

            var response = await ExecuteRequestAsync<Response<List<string>>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion

        #region GetServices
        /// <summary>
        /// Gets a list with all registered user services.
        /// </summary>
        /// <returns>The list with all registered user services.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public List<Service> GetServices()
        {
            return Task.Run(async () => await GetServicesAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets a list with all registered user services asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the list with all registered user services.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Service>> GetServicesAsync()
        {
            return await GetServicesAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets a list with all registered user services asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains the list with all registered user services.</returns>
        /// <exception cref="AuthenticationException">The client or user cannot be authenticated.</exception>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<List<Service>> GetServicesAsync(CancellationToken cancellationToken)
        {
            CheckAuthentication();

            var request = new Rest.RestRequest("api/services.json");
            var response = await ExecuteRequestAsync<Response<List<Service>>>(cancellationToken, request, AuthenticatorType.OAuth2);

            return response.Data;
        }
        #endregion


        #region GetInfo
        /// <summary>
        /// Gets combined informations for supported values.
        /// </summary>
        /// <returns>A info of available parameters.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Info GetInfo()
        {
            return Task.Run(async () => await GetInfoAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets combined informations for supported values asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains combined informations for supported values.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Info> GetInfoAsync()
        {
            return await GetInfoAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets combined informations for supported values asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains combined informations for supported values.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Info> GetInfoAsync(CancellationToken cancellationToken)
        {
            var request = new Rest.RestRequest("api/info.json");
            var response = await ExecuteRequestAsync<Response<Info>>(cancellationToken, request, AuthenticatorType.None);

            return response.Data;
        }
        #endregion

        #region GetAlgorithms
        /// <summary>
        /// Gets all supported audio algorithms.
        /// </summary>
        /// <returns>All supported audio algorithms.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Dictionary<string, Algorithm> GetAlgorithms()
        {
            return Task.Run(async () => await GetAlgorithmsAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets all supported audio algorithms asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all supported audio algorithms.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<string, Algorithm>> GetAlgorithmsAsync()
        {
            return await GetAlgorithmsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets all supported audio algorithms asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all supported audio algorithms.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<string, Algorithm>> GetAlgorithmsAsync(CancellationToken cancellationToken)
        {
            var request = new Rest.RestRequest("api/info/algorithms.json");
            var response = await ExecuteRequestAsync<Response<Dictionary<string, Algorithm>>>(cancellationToken, request, AuthenticatorType.None);

            return response.Data;
        }
        #endregion

        #region GetFileEndings
        /// <summary>
        /// Gets all supported file endings.
        /// </summary>
        /// <returns>All supported file endings.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Dictionary<string, List<string>> GetFileEndings()
        {
            return Task.Run(async () => await GetFileEndingsAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets all supported file endings asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all supported file endings.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<string, List<string>>> GetFileEndingsAsync()
        {
            return await GetFileEndingsAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets all supported file endings asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all supported file endings.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<string, List<string>>> GetFileEndingsAsync(CancellationToken cancellationToken)
        {
            var request = new Rest.RestRequest("api/info/file_endings.json");
            var response = await ExecuteRequestAsync<Response<Dictionary<string, List<string>>>>(cancellationToken, request, AuthenticatorType.None);

            return response.Data;
        }
        #endregion

        #region GetOutputFileTypes
        /// <summary>
        /// Gets all supported output file formats.
        /// </summary>
        /// <returns>All supported output file formats.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Dictionary<string, OutputFileType> GetOutputFileTypes()
        {
            return Task.Run(async () => await GetOutputFileTypesAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets all supported output file formats asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all supported output file formats.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<string, OutputFileType>> GetOutputFileTypesAsync()
        {
            return await GetOutputFileTypesAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets all supported output file formats asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all supported output file formats.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<string, OutputFileType>> GetOutputFileTypesAsync(CancellationToken cancellationToken)
        {
            var request = new Rest.RestRequest("api/info/output_files.json");
            var response = await ExecuteRequestAsync<Response<Dictionary<string, OutputFileType>>>(cancellationToken, request, AuthenticatorType.None);

            return response.Data;
        }
        #endregion

        #region GetProductionStatus
        /// <summary>
        /// Gets all status codes of an audio production.
        /// </summary>
        /// <returns>All status codes of an audio production.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Dictionary<ProductionStatus, string> GetProductionStatus()
        {
            return Task.Run(async () => await GetProductionStatusAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets all status codes of an audio production asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all status codes of an audio production.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<ProductionStatus, string>> GetProductionStatusAsync()
        {
            return await GetProductionStatusAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets all status codes of an audio production asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all status codes of an audio production.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<ProductionStatus, string>> GetProductionStatusAsync(CancellationToken cancellationToken)
        {
            var request = new Rest.RestRequest("api/info/production_status.json");
            var response = await ExecuteRequestAsync<Response<Dictionary<string, string>>>(cancellationToken, request, AuthenticatorType.None);

            var enumType = typeof(ProductionStatus);
            var data = response.Data.ToDictionary(p => (ProductionStatus)Enum.Parse(enumType, p.Key), p => p.Value);

            return data;
        }
        #endregion

        #region GetServiceTypes
        /// <summary>
        /// Gets all supported external services.
        /// </summary>
        /// <returns>All supported external services.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public Dictionary<string, ServiceType> GetServiceTypes()
        {
            return Task.Run(async () => await GetServiceTypesAsync()).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Gets all supported external services asynchronously.
        /// </summary>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all supported external services.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<string, ServiceType>> GetServiceTypesAsync()
        {
            return await GetServiceTypesAsync(CancellationToken.None);
        }

        /// <summary>
        /// Gets all supported external services asynchronously.
        /// </summary>
        /// <param name="cancellationToken">A cancellation token that can be used to cancel the work.</param>
        /// <returns>A task that represents the asynchronous operation. The value of the
        /// <em>TResult</em> parameter contains all supported external services.</returns>
        /// <exception cref="AuphonicException">A server side error occurred.</exception>
        public async Task<Dictionary<string, ServiceType>> GetServiceTypesAsync(CancellationToken cancellationToken)
        {
            var request = new Rest.RestRequest("api/info/service_types.json");
            var response = await ExecuteRequestAsync<Response<Dictionary<string, ServiceType>>>(cancellationToken, request, AuthenticatorType.None);

            return response.Data;
        }
        #endregion


        #region Protected Methods
        /// <summary>
        /// Raises the <see cref="RecieveResponse"/> event.
        /// </summary>
        /// <param name="response"></param>
        protected virtual void OnRecieveResponse(IRestResponse response)
        {
            RecieveResponse?.Invoke(this, new RestResponseEventArgs(response));
        }

        /// <summary>
        /// Raises the <see cref="SendRequest"/> event.
        /// </summary>
        /// <param name="request"></param>
        protected virtual void OnSendRequest(IRestRequest request)
        {
            SendRequest?.Invoke(this, new RestRequestEventArgs(request));
        }
        #endregion

        #region Private Methods
        private void CheckAuthentication()
        {
            if (AccessToken == null)
            {
                throw new AuthenticationException("No authentication credentials provided.");
            }
        }

        private T Deserialize<T>(IRestRequest request, IRestResponse response)
        {
            var serializer = (NewtonsoftJsonSerializer)request.JsonSerializer;
            T result = default(T);

            try
            {
                result = serializer.Deserialize<T>(response.Content);
            }
            catch (Exception ex)
            {
                throw new AuphonicException(null, ex.Message, response.StatusCode, response.Content, ex);
            }

            return result;
        }

        private async Task<T> ExecuteRequestAsync<T>(
            CancellationToken cancellationToken,
            IRestRequest request,
            AuthenticatorType authenticatorType,
            RequestType requestType = RequestType.Json)
        {
            switch (authenticatorType)
            {
                case AuthenticatorType.Basic:
                    _client.Authenticator = new HttpBasicAuthenticator(ClientId, ClientSecret);
                    break;
                case AuthenticatorType.OAuth2:
                    _client.Authenticator = new OAuth2AuthorizationRequestHeaderAuthenticator(AccessToken, "bearer");
                    break;
                default:
                    _client.Authenticator = null;
                    break;
            }

            switch (requestType)
            {
                case RequestType.Json:
                    request.RequestFormat = DataFormat.Json;
                    request.AddHeader("Content-Type", "application/json; charset=utf-8");
                    break;
            }

            OnSendRequest(request);
            var response = await _client.ExecuteTaskAsync(request, cancellationToken).ConfigureAwait(false);
            OnRecieveResponse(response);

            var result = default(T);

            try
            {
                result = Deserialize<T>(request, response);
            }
            catch (Exception ex)
            {
                if (response.StatusCode == HttpStatusCode.BadRequest ||
                    response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var error = Deserialize<ResponseError>(request, response);
                    var message = $"{error.Error}: {error.ErrorDescription}";

                    throw new AuthenticationException(message, ex);
                }
                else
                {
                    throw;
                }
            }

            if (response.StatusCode != HttpStatusCode.OK)
            {
                var resultType = result.GetType();
                var errorCode = response.StatusCode.ToString("D");
                var errorMessage = response.StatusDescription;

                if (resultType.IsGenericType &&
                    resultType.GetGenericTypeDefinition() == typeof(Response<object>).GetGenericTypeDefinition() &&
                    result != null)
                {
                    errorCode = (string)resultType.GetProperty("ErrorCode").GetValue(result);
                    errorMessage = (string)resultType.GetProperty("ErrorMessage").GetValue(result);

                    if (errorMessage.ToLower().Contains("token"))
                    {
                        throw new AuthenticationException(errorMessage);
                    }
                }
                else if (response.StatusCode == HttpStatusCode.BadRequest ||
                         response.StatusCode == HttpStatusCode.Unauthorized)
                {
                    var error = Deserialize<ResponseError>(request, response);
                    errorMessage = $"{error.Error}: {error.ErrorDescription}";

                    throw new AuthenticationException(errorMessage);
                }

                throw new AuphonicException(errorCode, errorMessage, response.StatusCode, response.Content);
            }

            return result;
        }
        #endregion
    }
}
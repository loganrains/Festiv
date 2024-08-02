using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using SpotifyAPI.Web;
using Festiv.Models;

namespace Festiv.Services
{
    public class SpotifyService
    {
        private string _accessToken;
        private readonly string _clientId;
        private readonly string _clientSecret;
        private readonly string _redirectUri;
        private readonly string _playlistId;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IConfiguration _configuration;

        public SpotifyService(IHttpClientFactory httpClientFactory, IConfiguration configuration)
        {
            _httpClientFactory = httpClientFactory;
            _configuration = configuration;
            _clientId = configuration["Spotify:ClientId"];
            _clientSecret = configuration["Spotify:ClientSecret"];
            _redirectUri = configuration["Spotify:RedirectUri"];
            _playlistId = configuration["Spotify:PlaylistId"];
        }

        public async Task<string> GetAccessTokenAsync(string code, string redirectUri)
        {
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            var postData = new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("grant_type", "authorization_code"),
                new KeyValuePair<string, string>("code", code),
                new KeyValuePair<string, string>("redirect_uri", redirectUri),
                new KeyValuePair<string, string>("client_id", _clientId),
                new KeyValuePair<string, string>("client_secret", _clientSecret)
            };

            tokenRequest.Content = new FormUrlEncodedContent(postData);
    
            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(tokenRequest);
            var responseContent = await response.Content.ReadAsStringAsync();
    
            var tokenResponse = System.Text.Json.JsonSerializer.Deserialize<Dictionary<string, object>>(responseContent);
    
            return tokenResponse["access_token"].ToString();
        }


        public async Task<bool> EnsureAccessTokenAsync()
        {
            if (string.IsNullOrEmpty(_accessToken))
            {
                return await RefreshAccessTokenAsync();
            }
            return true;
        }

        private async Task<bool> RefreshAccessTokenAsync()
        {
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token");
            var credentials = Convert.ToBase64String(System.Text.Encoding.UTF8.GetBytes($"{_clientId}:{_clientSecret}"));
            tokenRequest.Headers.Authorization = new AuthenticationHeaderValue("Basic", credentials);

            var content = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                {"grant_type", "client_credentials"}
            });
            tokenRequest.Content = content;

            var client = _httpClientFactory.CreateClient();
            var response = await client.SendAsync(tokenRequest);
            if (response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                var tokenResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(responseContent);
                _accessToken = tokenResponse["access_token"].ToString();
                return true;
            }
            return false;
        }

        public async Task AddTrackToPlaylist(string accessToken, string trackUri)
        {
            var spotify = new SpotifyClient(accessToken);
            var playlistId = "37i9dQZF1DWXti3N4Wp5xy?si=cf044e8a63aa467c"; // Example playlist ID
            await spotify.Playlists.AddItems(playlistId, new PlaylistAddItemsRequest(new List<string> { trackUri }));
        }

        public string GetSpotifyAuthorizationUrl()
        {
            var scope = "playlist-modify-public";
            return $"https://accounts.spotify.com/authorize?client_id={_clientId}&response_type=code&redirect_uri={Uri.EscapeDataString(_redirectUri)}&scope={Uri.EscapeDataString(scope)}";
        }

        public async Task<string> GetTrackAsync(string trackId, string accessToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync($"https://api.spotify.com/v1/tracks/{trackId}");

            // Log response content
            if (!response.IsSuccessStatusCode)
            {
                var responseContent = await response.Content.ReadAsStringAsync();
                throw new HttpRequestException($"Request failed with status code {response.StatusCode}: {responseContent}");
            }

            var content = await response.Content.ReadAsStringAsync();
            var trackInfo = JObject.Parse(content);
            return trackInfo["name"].ToString();
        }


        public async Task<SpotifyTokenResponse> GetPlaylistAsync(string accessToken)
        {
            var client = _httpClientFactory.CreateClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await client.GetAsync("https://api.spotify.com/v1/me/playlists");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SpotifyTokenResponse>(content);
            }
            return null;
        }
    }
}

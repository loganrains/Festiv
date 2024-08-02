using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Festiv.Models;

namespace Festiv.Services
{
    public class SpotifyService
    {
        private string _accessToken;
        private readonly string _clientId = "5275b4abc3474605bec58bb4c9d83d23";
        private readonly string _clientSecret = "ac77b16089d94d3e8f028e77f2e4e1aa";
        private readonly string _redirectUri;
        private readonly string _playlistId;
        private readonly HttpClient _httpClient;

        public SpotifyService(HttpClient httpClient, IConfiguration configuration)
        {
            _httpClient = httpClient;
            _clientId = configuration["Spotify:ClientId"];
            _clientSecret = configuration["Spotify:ClientSecret"];
            _redirectUri = configuration["Spotify:RedirectUri"];
            _playlistId = configuration["Spotify:PlaylistId"];
        }

        public async Task<string> GetAccessTokenAsync(string code, string redirectUri)
        {
            var tokenRequest = new HttpRequestMessage(HttpMethod.Post, "https://accounts.spotify.com/api/token")
            {
                Content = new FormUrlEncodedContent(new Dictionary<string, string>
                {
                    {"grant_type", "authorization_code"},
                    {"code", code},
                    {"redirect_uri", _redirectUri},
                    {"client_id", _clientId},
                    {"client_secret", _clientSecret}
                })
            };

            var response = await _httpClient.SendAsync(tokenRequest);
            response.EnsureSuccessStatusCode();

            var responseContent = await response.Content.ReadAsStringAsync();
            var tokenResponse = JsonConvert.DeserializeObject<SpotifyTokenResponse>(responseContent);

            return tokenResponse.AccessToken;
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

            var response = await _httpClient.SendAsync(tokenRequest);
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
            var request = new HttpRequestMessage(HttpMethod.Post, $"https://api.spotify.com/v1/playlists/{_playlistId}/tracks?uris={trackUri}");
            request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            await _httpClient.SendAsync(request);
        }

        public string GetSpotifyAuthorizationUrl()
        {
            var scope = "playlist-modify-public";
            return $"https://accounts.spotify.com/authorize?client_id={_clientId}&response_type=code&redirect_uri={Uri.EscapeDataString(_redirectUri)}&scope={Uri.EscapeDataString(scope)}";
        }

    public async Task<string> GetTrackAsync(string trackId, string accessToken)
    {
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
        var response = await _httpClient.GetAsync($"https://api.spotify.com/v1/tracks/{trackId}");

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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var response = await _httpClient.GetAsync("https://api.spotify.com/v1/me/playlists");

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                return JsonConvert.DeserializeObject<SpotifyTokenResponse>(content);
            }
            return null;
        }
    }
}

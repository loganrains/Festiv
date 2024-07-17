// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using SpotifyAPI.Web;
// using System.Net.Http.Headers;
// using System.Reflection.Metadata.Ecma335;
// using System.Threading.Tasks;

// namespace Festiv.Services
// {
//     public class SpotifyAccountService : ISpotifyAccountService
//     {
//         public readonly HttpClient _httpClient;

//         public SpotifyAccountService(HttpClient httpClient)
//         {
//             _httpClient = httpClient;
//         }

//         public Task<string> GetToken(string clientId, string clientSecret)
//         {
//             var request = HttpRequestMessage(HttpMethod.Post, "token");

//             request.Headers.Authorization = new AuthenticationHeaderValue(
//                 "Basic", Convert.Tobase64String(Encoding.UTF8.GetBytes($"{clientId}:{clientSecret}")));
//         }
//     }
// }
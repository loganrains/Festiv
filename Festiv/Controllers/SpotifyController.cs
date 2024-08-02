using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using Festiv.Models;
using Festiv.Services;
using Microsoft.Extensions.Configuration;
using System.Configuration;
using System.Threading.Tasks;

namespace Festiv.Controllers
{
    public class SpotifyController : Controller
    {
        private readonly SpotifyService _spotifyService;
        // private object partyId;
        [TempData]
        public int PartyId {get; set;}

        public SpotifyController(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        [HttpGet("/spotify/login/{partyId}")]
        public IActionResult Login(int partyId)
        {
            Console.WriteLine($"Spotify Login {partyId}");
            PartyId = partyId;
            string clientId = "5275b4abc3474605bec58bb4c9d83d23";
            string redirectUri = Url.Action("Callback", "Spotify", null, Request.Scheme);
            Console.WriteLine($"{Uri.EscapeDataString(redirectUri)}");
            string scopes = "playlist-modify-public";
            string spotifyUrl = $"https://accounts.spotify.com/authorize?client_id={clientId}&response_type=code&redirect_uri={Uri.EscapeDataString(redirectUri)}&scope={Uri.EscapeDataString(scopes)}";

            return Redirect(spotifyUrl);
        }

        [HttpGet("/Callback")]
        public async Task<IActionResult> Callback(string code)
        {  
            string redirectUri = Url.Action("Callback", "Spotify", null, Request.Scheme);
            var accessToken = await _spotifyService.GetAccessTokenAsync(code, redirectUri);

            // Save access token for use in adding tracks
            HttpContext.Session.SetString("SpotifyAccessToken", accessToken);

            // partyId = TempData["PartyId"];
            Console.WriteLine($"Callback... {PartyId}");

            return Redirect($"/PartyDetails/{PartyId}");
        }   


        public IActionResult Jukebox()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddTrack(string trackUri)
        {
            var accessToken = HttpContext.Session.GetString("SpotifyAcessToken");
            if(string.IsNullOrEmpty(accessToken))
            {
                return RedirectToAction("Index");
            }

            await _spotifyService.AddTrackToPlaylist(accessToken, trackUri);
            return RedirectToAction("Jukebox");
        }
    }
}
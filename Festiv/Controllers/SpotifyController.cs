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

        public SpotifyController(SpotifyService spotifyService)
        {
            _spotifyService = spotifyService;
        }

        public IActionResult PartyDetails(string partyId)
        {
            return View();
        }

        public IActionResult Login()
        {
            string clientId = "5275b4abc3474605bec58bb4c9d83d23";
            string redirectUri = Url.Action("Callback", "Spotify", null, Request.Scheme);
            string scopes ="user-read-private user-read-email";
            string spotifyUrl = $"https://accounts.spotify.com/authorize?client_id={clientId}&response_type=code&redirect_uri={Uri.EscapeDataString(redirectUri)}&scope={Uri.EscapeDataString(scopes)}";

            return Redirect(spotifyUrl);
        }


        public async Task<IActionResult> Callback(string code)
        {
            var accessToken = await _spotifyService.GetAccessTokenAsync(code, Url.Action("Callback", "Spotify", null, Request.Scheme));

            // Save access token for use in adding tracks
            HttpContext.Session.SetString("SpotifyAccessToken", accessToken);

            return RedirectToAction("PartyDetails/{partyId}");
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
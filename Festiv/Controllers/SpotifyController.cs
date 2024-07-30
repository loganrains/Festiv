using Microsoft.AspNetCore.Mvc;
using SpotifyAPI.Web;
using System.Configuration;
using System.Threading.Tasks;

namespace Festiv.Controllers
{
    public class SpotifyController : Controller
    {
        private readonly SpotifyClientConfig _spotifyConfig;

        public SpotifyController(IConfiguration configuration)
        {
            _spotifyConfig = SpotifyClientConfig
                .CreateDefault()
                .WithAuthenticator(new ClientCredentialsAuthenticator(
                    configuration["Spotify:ClientId"],
                    configuration["Spotify:ClientSecret"]));
        }

        public IActionResult Authorize()
        {
            var loginRequest = new LoginRequest(
                new Uri("http://localhost:5196/Party/PartyDetails/{partyId}"),
                configuration["Spotify:ClientId"],
                LoginRequest.ResponseType.Code
            )
            {
                Scope = new[] { Scopes.UserReadPlaybackState, Scopes.UserModifyPlaybackState }
            };
            return Redirect(loginRequest.ToUri().ToString());
        }   

        public async Task<IActionResult> CallBack(string code)
        {
            var responce = await new OAuthClient().RequestToken(
                new AuthorizationCodeTokenRequest(
                    configuration["Spotify:ClientId"],
                    configuration["Spotify:ClientSecret"],
                    code,
                    new Uri("http://localhost:5196/Party/PartyDetails/{partyId}")
                )
            );

            return RedirectToAction("Party", "PartyDetails", new { id = partyId})
        }
    }
}
using SpotifyAPI.Web;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Festiv.Services
{
    public class SpotifyService
    {
        public readonly ISpotifyClient _spotifyClient;

        public SpotifyService(ISpotifyClient spotifyClient)
        {
            _spotifyClient = spotifyClient;
        }

        
    }

}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SpotifyAPI.Web;
using System.Net.Http.Headers;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace Festiv.Services
{
    public class SpotifyService 
    {
        public readonly ISpotifyClient _spotifyClient;

        public SpotifyService(string accessToken)
        {
            _spotifyClient = new SpotifyClient(accessToken);
        }

        public async Task<FullTrack> GetTrackAsync(string trackId)
        {
            return await -SpotifyClient.Tracks.Get(trackId);
        }
    }
}
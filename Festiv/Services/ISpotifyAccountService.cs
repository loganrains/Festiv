using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Festiv.Services
{
    public interface ISpotifyAccountService
    {
        Task<string>GetToken(string clientId, string clientSecret);
    }
}
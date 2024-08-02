using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace Festiv.Models
{
    public class PlaylistItem
    {
        [JsonProperty("id")]
        public string? Id { get; set; }
        [JsonProperty("name")]
        public string? Name { get; set; }
        [JsonProperty("tracks")]
        public TracksInfo Tracks { get; set; }
    }

    public class TracksInfo
    {
        [JsonProperty("total")]
        public int Total { get; set; }
    }
}
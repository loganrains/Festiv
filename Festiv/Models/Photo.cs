namespace Festiv.Models
{
    public class Photo
    {
        public string? Link { get; set; }

        public string? AltText { get; set; }

        public int? PartyDetailsId { get; set; }

        public PartyDetails? Details { get; set; }
    
        public int Id { get; set; }

        public Photo()
        {
        }

        public Photo(string link, string altText, int partyDetailsId)
        {
        Link = link;
        AltText = altText;
        PartyDetailsId = partyDetailsId;
        }

        public override string ToString()
        {
            return AltText;
        }
    }
}
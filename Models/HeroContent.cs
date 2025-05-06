namespace TravelApp.Models
{
    public class HeroContent
    {
        public int Id { get; set; }
        public string ButtonText { get; set; } = string.Empty;
        public string Heading { get; set; } = string.Empty;
        public string HighlightedWord { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Image1Url { get; set; } = string.Empty;
        public string Image2Url { get; set; } = string.Empty;
        public string VideoUrl { get; set; } = string.Empty;
        public string VideoPosterUrl { get; set; } = string.Empty;
    }
}
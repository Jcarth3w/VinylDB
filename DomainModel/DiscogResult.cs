namespace DomainModel
{
    public class DiscogsResult
    {
        public string Title { get; set; }

        public List<string> Genre { get; set; }

        public List<Artist> Artists { get; set; }  // Will use Artists[0].Name as Artist

        public string Cover_Image { get; set; }
    }
}


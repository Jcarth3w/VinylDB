using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DomainModel
{
    public class Record
    {
        [Key]
        public int Id { get; set;}

        public string Title {get; set;}

        [ForeignKey("Artist")]
        public int ArtistId {get; set;}

        public string Artist {get; set;}

        public string Genre {get; set;}

        public string Img {get; set;}

        public int NumSongs {get; set;}

        public int Length {get; set;}

        public int Rating {get; set;}

        public Record() {}


        public Record(int id, string title, int artistId, string artist, string genre, string img, int numSongs, int length, int rating)
        {
            if (string.IsNullOrEmpty(title))
            {
                Id = 0;
                Title = null;
                ArtistId = 0;
                Artist = null;
                Genre = null;
                Img = null;
                NumSongs = 0;
                Length = 0;
                Rating = 0;
            }
            else
            {
                Id = id;
                Title = title;
                ArtistId = artistId;
                Artist = artist;
                Genre = genre;
                Img = (img != null && (img.EndsWith(".jpg") || img.EndsWith(".png") || img.EndsWith(".jpeg"))) ? img : null;
                NumSongs = numSongs;
                Length = length;
                Rating = rating;
            }
        }
    }
}

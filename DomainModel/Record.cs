using System.ComponentModel.DataAnnotations;


namespace DomainModel
{
    public class Record
    {
        [Key]
        public int Id { get; set;}

        public string Title {get; set;}

        public string Artist {get; set;}

        public string Genre {get; set;}

        public string Img {get; set;}

        public int NumSongs {get; set;}

        public int Length {get; set;}

        public int Rating {get; set;}


        public Record(int id, string title, String artist, string genre, string img, int numSongs, int length, int rating)
        {
            Id = id;
            Title = title;
            Artist = artist;
            Genre = genre;
            Img = img;
            NumSongs = numSongs;
            Length = length;
            Rating = rating;
        }
    }


}
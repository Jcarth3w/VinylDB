using System.ComponentModel.DataAnnotations;


namespace DomainModel
{
    public class Record
    {
        [Key]
        public int Id { get; set;}

        public String Title {get; set;}

        public String Artist {get; set;}

        public String Genre {get; set;}

        public String Img {get; set;}

        public int NumSongs {get; set;}

        public int Length {get; set;}

        public int Rating {get; set;}
    }

    public Record()
    {}


    public Record(int id, String title, String artist, String genre, String img, int numSongs, int length, int rating)
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
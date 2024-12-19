using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Artist
    {
        [Key]
        public String Name;

        public List<Record> Records {get; set;}

        public List<string> Genres {get; set;}

        public String Img {get; set;}


        public Artist(String name, List<Record> records, List<String> genres, String img)
        {
            Name  = name;
            Records = records;
            Genres = genres;
            Img = img;
        }

    }



}
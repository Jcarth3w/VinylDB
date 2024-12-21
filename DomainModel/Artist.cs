using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Artist
    {
        [Key]
        public string Name;

        public List<Record> Records {get; set;}

        public List<string> Genres {get; set;}

        public string Img {get; set;}


        public Artist(String name, List<Record> records, List<String> genres, String img)
        {
            Name  = name;
            Records = records;
            Genres = genres;
            Img = (img != null && (img.EndsWith(".jpg") || img.EndsWith(".png") || img.EndsWith(".jpeg"))) ? img : null;
        }

    }



}
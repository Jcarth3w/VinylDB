using System.ComponentModel.DataAnnotations;

namespace DomainModel
{
    public class Artist
    {
        [Key]
        public int Id { get; set; }

        public string Name {get; set;}

        public List<Record> Records {get; set;}

        public List<string> Genres {get; set;}

        public string Img {get; set;}

        public Artist() { }


        public Artist(int id, string name, List<Record> records, List<String> genres, String img)
        {
            Id = id;
            Name  = name;
            Records = records;
            Genres = genres;
            Img = (img != null && (img.EndsWith(".jpg") || img.EndsWith(".png") || img.EndsWith(".jpeg"))) ? img : null;
        }

    }



}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class DiscogRecord
    {
        public string Title {get; set;}
        public string Artist {get; set;}
        public List<string> Genre {get; set;}

        public string Cover_Image {get; set;}


        public  DiscogRecord () {}


        public DiscogRecord (string title, string artist, List<string> genre, string img) 
        {
            Title = title;
            Artist = artist;
            Genre = genre;
            Cover_Image = img;
        }

    }

}
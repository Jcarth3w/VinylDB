using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class DiscogRecord
    {
        public string Title {get; set;}
        public string Artist {get; set;}
        public string Img {get; set;}
    }
    public DiscogRecord () {}


    public DiscogRecord (string title, string artist, string img) 
    {
        Title = title,
        Artist = artist,
        Img = img 
    }


}
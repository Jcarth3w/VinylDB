using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DomainModel
{
    public class DiscogRecord
    {
        public string Title {get; set;}
        public string Artist {get; set;}
        public string Genre {get; set;}


        public  DiscogRecord () {}


        public DiscogRecord (string title, string artist, string genre) 
        {
            Title = title;
            Artist = artist;
            Genre = genre;
        }

    }

}
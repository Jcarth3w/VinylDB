using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace VinylLibrarian.Models
{
    public class DiscogViewModel
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public List<string> Genre { get; set; }

        public string Img { get; set; }


        public static DiscogViewModel FromDiscogRecord(DiscogRecord discogRecord)
        {
            if (discogRecord == null)
                return new DiscogViewModel();

            return new DiscogViewModel
            {
                Title = discogRecord.Title,
                Artist = discogRecord.Artist,
                Genre = discogRecord.Genre,
                Img = discogRecord.Cover_Image,
            };
        }
    }
}
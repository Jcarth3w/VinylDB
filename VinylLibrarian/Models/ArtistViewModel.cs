using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace VinylLibrarian.Models
{
    public class ArtistViewModel
    {
        public string Name { get; set; }

        public List<Record> Records { get; set; }

        public List<string> Genres { get; set; }

        public string Img { get; set; }

        public static ArtistViewModel FromRecord(Artist artist)
        {
            return new ArtistViewModel
            {
                Name = artist.Name,
                Records = artist.Records,
                Genres = artist.Genres,
                Img = artist.Img,

            };
        }
    }
}
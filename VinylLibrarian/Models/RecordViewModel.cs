using System.Collections.Generic;
using System.Linq;
using System.ComponentModel.DataAnnotations;
using DomainModel;

namespace VinylLibrarian.Models
{
    public class RecordViewModel
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Genre { get; set; }

        public string Img { get; set; }

        public int NumSongs { get; set; }
        
        public int Length { get; set; }

        public int Rating { get; set; }

        public static RecordViewModel FromRecord(Record reocrd)
        {
            return new RecordViewModel
            {
                Title = reocrd.Title,
                Artist = reocrd.Artist,
                Genre = reocrd.Genre,
                Img = reocrd.Img,
                NumSongs = reocrd.NumSongs,
                Length = reocrd.Length,
                Rating = reocrd.Rating
            };
        }
    }
}
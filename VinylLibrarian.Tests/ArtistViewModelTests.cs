using System.Collections.Generic;
using VinylLibrarian.Models;
using Xunit;

namespace VinylLibrarian.Tests
{
    public class ArtistViewModelTests
    {
        [Fact]
        public void FromArtist_CorrectViewModwl()
        {
            var artist = new DomainModel.Artist(
            
                "Yo mama",
                new List<DomainModel.Record>
                {
                    new DomainModel.Record(1, "The fartening", "Yo mama", "Rock", "/images/album1.jpg", 10, 40, 5),
                    new DomainModel.Record(2, "Stink", "Yo mama", "Jazz", "/images/album2.jpg", 8, 35, 4)
                },
                new List<string> { "Rock", "Jazz" },
                "/images/test.jpg"
            );

            var viewModel = ArtistViewModel.FromArtist(artist);

            Assert.Equal("Yo mama", viewModel.Name);
            Assert.Equal(2, artist.Records.Count);
            Assert.Equal("The fartening", artist.Records[0].Title);
            Assert.Equal(5, artist.Records[0].Rating);
            Assert.Equal(2, viewModel.Genres.Count);
            Assert.Equal("/images/test.jpg", viewModel.Img);
        }


        [Fact]
        public void FromArtist_NullArtistReturnsNull()
        {
            var artist = new DomainModel.Artist(
                null, 
                null, 
                null, 
                null
            );

            var viewModel = ArtistViewModel.FromArtist(artist);

            Assert.Null(viewModel.Name);
            Assert.Null(viewModel.Records); 
            Assert.Null(viewModel.Genres);  
            Assert.Null(viewModel.Img);
        }

        [Fact]
        public void FromArtist_RecordsMappedCorrectly()
        {
            var artist = new DomainModel.Artist(
                "Sample Artist",
                new List<DomainModel.Record>
                {
                    new DomainModel.Record(1, "Album1", "Sample Artist", "Rock", "/images/album1.jpg", 10, 40, 5),
                    new DomainModel.Record(2, "Album2", "Sample Artist", "Jazz", "/images/album2.jpg", 8, 35, 4)
                },
                new List<string> { "Rock", "Jazz" },
                "/images/test.jpg"
            );

            var viewModel = ArtistViewModel.FromArtist(artist);

            Assert.Equal(2, viewModel.Records.Count);
            Assert.Equal("Album1", viewModel.Records[0].Title);
            Assert.Equal("Rock", viewModel.Records[0].Genre);
            Assert.Equal("/images/album1.jpg", viewModel.Records[0].Img);
        }

        [Fact]
        public void FromArtist_GenresMappedCorrectly()
        {
            var artist = new DomainModel.Artist(
                "Test Artist",
                new List<DomainModel.Record>(),
                new List<string> { "Rock", "Pop" },
                "/images/test.jpg"
            );

            var viewModel = ArtistViewModel.FromArtist(artist);

            Assert.Equal(2, viewModel.Genres.Count);
            Assert.Contains("Rock", viewModel.Genres);
            Assert.Contains("Pop", viewModel.Genres);
        }


        [Fact]
        public void FromArtist_ImageUrlCorrectlySet()
        {
            var artist = new DomainModel.Artist(
                "Image Test Artist",
                new List<DomainModel.Record>(),
                new List<string>(),
                "/images/test.jpg"
            );

            var viewModel = ArtistViewModel.FromArtist(artist);

            Assert.Equal("/images/test.jpg", viewModel.Img);
        }


        [Fact]
        public void FromArtist_NoRecordsOrGenres_ReturnsEmptyLists()
        {
            var artist = new DomainModel.Artist(
                "",
                new List<DomainModel.Record>(),
                new List<string>(),
                null
            );

            var viewModel = ArtistViewModel.FromArtist(artist);
            Assert.Equal("", viewModel.Name);
            Assert.Empty(viewModel.Records);
            Assert.Empty(viewModel.Genres);
            Assert.Null(viewModel.Img);
        }
    }
}


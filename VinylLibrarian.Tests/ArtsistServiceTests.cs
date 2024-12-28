using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using DomainModel;
using VinylLibrarian.Services.Classes;


namespace VinylLibrarian.Tests
{
    public class ArtistServiceTests
    {
        private readonly ArtistService artistService;
        private readonly Mock<ILogger<ArtistService>> mockLogger;

        public ArtistServiceTests()
        {
            // Shared setup for mocks
            var mockArtists = new List<DomainModel.Artist>
            {
                new DomainModel.Artist(1,
                "Sample Artist",
                new List<DomainModel.Record>
                {
                    new DomainModel.Record(1, "Album1", 1, "Sample Artist", "Rock", "/images/album1.jpg", 10, 40, 5),
                    new DomainModel.Record(2, "Album2", 2, "Sample Artist", "Jazz", "/images/album2.jpg", 8, 35, 4)
                },
                new List<string> { "Rock", "Jazz" },
                "/images/test.jpg"
            ),
                new DomainModel.Artist(2, "Test Artist", new List<DomainModel.Record>(), new List<string> { "Rock", "Pop" },"/images/test.jpg")
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<DomainModel.Artist>>();
            mockDbSet.As<IQueryable<DomainModel.Artist>>().Setup(m => m.Provider).Returns(mockArtists.Provider);
            mockDbSet.As<IQueryable<DomainModel.Artist>>().Setup(m => m.Expression).Returns(mockArtists.Expression);
            mockDbSet.As<IQueryable<DomainModel.Artist>>().Setup(m => m.ElementType).Returns(mockArtists.ElementType);
            mockDbSet.As<IQueryable<DomainModel.Artist>>().Setup(m => m.GetEnumerator()).Returns(mockArtists.GetEnumerator());

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(c => c.Artist).Returns(mockDbSet.Object);

            mockLogger = new Mock<ILogger<ArtistService>>();

            // Initialize the service to be reused in tests
            artistService = new ArtistService(mockLogger.Object, mockContext.Object);
        }

        [Fact]
        public void GetAllArtists_ReturnsAllArtists()
        {
            var result = artistService.getAllArtists();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Sample Artist", result[0].Name);
            Assert.Equal(2, result[0].Records.Count);
            Assert.Equal("Rock", result[0].Genres[0]);
            Assert.Equal("/images/test.jpg", result[0].Img);

        }


        [Fact]
        public void FindArtistById_ReturnsCorrectArtist()
        {
            var result = artistService.findArtistById(1);

            Assert.NotNull(result);
            Assert.Equal("Sample Artist", result.Name);
            Assert.Equal(2, result.Records.Count);
            Assert.Equal("Rock", result.Genres[0]);
            Assert.Equal("/images/test.jpg", result.Img);
        }

        [Fact]
        public void FindArtistById_IdNotFound_ReturnsNull()
        {
            var result = artistService.findArtistById(3);
            Assert.Null(result);
        }
        

        [Fact]
        public void FindArtistById_IdInvalid_ReturnsNull()
        {
            var result = artistService.findArtistById(-1);
            Assert.Null(result);
        }

        
    }
}


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
    public class RecordServiceTests
    {
        private readonly RecordService recordService;

        public RecordServiceTests()
        {
            // Shared setup for mocks
            var mockRecords = new List<DomainModel.Record>
            {
                new DomainModel.Record(1, "Album1", 1, "Artist1", "Rock", "/images/album1.jpg", 10, 40, 5),
                new DomainModel.Record(2, "Album2", 2, "Artist2", "Jazz", "/images/album2.jpg", 8, 35, 4)
            }.AsQueryable();

            var mockDbSet = new Mock<DbSet<DomainModel.Record>>();
            mockDbSet.As<IQueryable<DomainModel.Record>>().Setup(m => m.Provider).Returns(mockRecords.Provider);
            mockDbSet.As<IQueryable<DomainModel.Record>>().Setup(m => m.Expression).Returns(mockRecords.Expression);
            mockDbSet.As<IQueryable<DomainModel.Record>>().Setup(m => m.ElementType).Returns(mockRecords.ElementType);
            mockDbSet.As<IQueryable<DomainModel.Record>>().Setup(m => m.GetEnumerator()).Returns(mockRecords.GetEnumerator());

            var mockContext = new Mock<IDataContext>();
            mockContext.Setup(c => c.Record).Returns(mockDbSet.Object);

            var mockLogger = new Mock<ILogger<RecordService>>();

            // Initialize the service to be reused in tests
            recordService = new RecordService(mockLogger.Object, mockContext.Object);
        }


        [Fact]
        public void GetAllRecords_ReturnsAllRecords()
        {
            var result = recordService.getAllRecords();

            Assert.NotNull(result);
            Assert.Equal(2, result.Count);
            Assert.Equal("Album1", result[0].Title);
            Assert.Equal("Artist1", result[0].Artist);
            Assert.Equal("Rock", result[0].Genre);
            Assert.Equal("/images/album1.jpg", result[0].Img);
        }

        [Fact]
        public void FindRecordById_ReturnsCorrectRecord()
        {
            var result = recordService.findRecordById(1);

            Assert.NotNull(result);
            Assert.Equal("Album1", result.Title);
            Assert.Equal("Artist1", result.Artist);
            Assert.Equal("Rock", result.Genre);
            Assert.Equal("/images/album1.jpg", result.Img);
            Assert.Equal(10, result.NumSongs);
            Assert.Equal(40, result.Length);
            Assert.Equal(5, result.Rating);
        }

        [Fact]
        public void GetRecordById_IdNotFound_ReturnsNull()
        {
            var result = recordService.findRecordById(3);
        
            Assert.Null(result);
        }
    }
}
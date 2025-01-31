using System.Collections.Generic;
using VinylLibrarian.Models;
using Xunit;

namespace VinylLibrarian.Tests
{
    public class RecordViewModelTests
    {
        [Fact]
        public void FromRecord_CorrectViewModel()
        {
            var record = new DomainModel.Record(1, "The fartening", 1, "Yo mama", "Rock", "/images/test.jpg", 10, 40, 5);

            var viewModel = RecordViewModel.FromRecord(record);

            Assert.Equal("The fartening", viewModel.Title);
            Assert.Equal("Yo mama", viewModel.Artist);
            Assert.Equal("Rock", viewModel.Genre);
            Assert.Equal("/images/test.jpg", viewModel.Img);
            Assert.Equal(10, viewModel.NumSongs);
            Assert.Equal(40, viewModel.Length);
            Assert.Equal(5, viewModel.Rating);

        }

        [Fact]
        public void FromRecord_AssignsNull_WhenNoTitle()
        {
            var record = new DomainModel.Record(1, "", 0, "absdasfdsdfadgf", "ASdijhsdfja", "asdfgdds", 22352462, 111111, 4212);

            var viewModel = RecordViewModel.FromRecord(record);

            Assert.Null(viewModel.Title);
            Assert.Null(viewModel.Artist); 
            Assert.Null(viewModel.Genre);  
            Assert.Null(viewModel.Img);
            Assert.Equal(0, viewModel.NumSongs);
            Assert.Equal(0, viewModel.Length);
            Assert.Equal(0, viewModel.Rating);

        }

        [Fact]
        public void FromRecord_ImageUrlCorrectlySet()
        {
            var record = new DomainModel.Record(1, "The fartening", 1, "Yo mama", "Rock", "/images/test.jpg", 10, 40, 5);

            var viewModel = RecordViewModel.FromRecord(record);

            Assert.Equal("/images/test.jpg", viewModel.Img);
        }
        
        [Fact]
        public void FromRecord_InvalidImageExtension_ReturnsNullImg()
        {
            var record = new DomainModel.Record(1, "The fartening", 1, "Yo mama", "Rock", "/images/test", 10, 40, 5);

            var viewModel = RecordViewModel.FromRecord(record);

            Assert.Null(viewModel.Img);
        }
    }
}

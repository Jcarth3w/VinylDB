using DomainModel;
using Microsoft.Extensions.Logging;
using DomainModel;

namespace VinylLibrarian.Services
{
    public class RecordService
    {
        private readonly ILogger<RecordService> logger;

        private DataContext db;

        public List<Record> records;

        public RecordService(ILogger<RecordService> logger)
        {
            this.logger = logger;
        } 

        public List<Record> getAllRecords()
        {
            logger.LogInformation("Getting all records...");
            db = new DataContext();
            records = db.Record
                .Select(r => new DomainModel.Record(r.Id, r.Title, r.ArtistId, r.Artist, r.Genre, r.Img, r.NumSongs, r.Length, r.Rating)).ToList();
            return records;
        }

    }
}
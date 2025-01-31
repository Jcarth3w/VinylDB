using DomainModel;
using Microsoft.Extensions.Logging;
using DomainModel;
using VinylLibrarian.Services.Interfaces;

namespace VinylLibrarian.Services.Classes
{
    public class RecordService : IRecordServices
    {
        private readonly ILogger<RecordService> logger;
        private readonly IDataContext db;

        public List<Record> records;

        public RecordService(ILogger<RecordService> logger, IDataContext db)
        {
            this.logger = logger;
            this.db = db;
        } 

        public List<Record> getAllRecords()
        {
            logger.LogInformation("Getting all records...");
            return db.Record
                .Select(r => new Record(r.Id, r.Title, r.ArtistId, r.Artist, r.Genre, r.Img, r.NumSongs, r.Length, r.Rating))
                .ToList();
        }

        public Record findRecordById(int id)
        {
            logger.LogInformation("Getting record of ID: {Id}" + id);
            return db.Record
                .SingleOrDefault(r => r.Id == id);
        }
    }
}
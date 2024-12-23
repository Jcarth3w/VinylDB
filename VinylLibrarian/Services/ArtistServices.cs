using DomainModel;
using Microsoft.Extensions.Logging;
using DomainModel;

namespace VinylLibrarian.Services
{
    public class ArtistService
    {
        private readonly ILogger<ArtistService> logger;
        private readonly IDataContext db;

        public List<Artist> artists;

        public ArtistService(ILogger<ArtistService> logger, IDataContext db)
        {
            this.logger = logger;
            this.db = db;
        } 

        public List<Artist> getAllArtists()
        {
            logger.LogInformation("Getting all artists...");
            return db.Artist
                .Select(r => new Artist(r.Id, r.Name, r.Records, r.Genres, r.Img))
                .ToList();
        }

        public Artist findArtistById(int id)
        {
            logger.LogInformation("Finding artist of ID: {Id}", id);
            return db.Artist
                .SingleOrDefault(a => a.Id == id);
        }
    }
}
using DomainModel;


namespace VinylLibrarian.Services.Interfaces
{
    public interface IArtistServices
    {
        public List<Artist> getAllArtists();

        public Artist findArtistById(int id);
    }
}
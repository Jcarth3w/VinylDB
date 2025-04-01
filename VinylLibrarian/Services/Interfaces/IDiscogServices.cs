using System.Collections.Generic;
using System.Threading.Tasks;
using DomainModel;


namespace VinylLibrarian.Services.Interfaces
{
    public interface IDiscogServices
    {
        Task<List<DiscogRecord>> SearchAlbumsAsync(string query, int limit, int page);
    }
}


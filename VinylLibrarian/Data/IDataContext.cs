using Microsoft.EntityFrameworkCore;

namespace DomainModel
{
    public interface IDataContext
    {
        DbSet<Record> Record { get; set; }

        DbSet<Artist>  Artist {get; set;}
    }
}
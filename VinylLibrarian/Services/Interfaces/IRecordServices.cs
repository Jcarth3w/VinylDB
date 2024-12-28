using DomainModel;

namespace VinylLibrarian.Services.Interfaces
{
    public interface IRecordServices
    {
        public List<Record> getAllRecords();

        public Record findRecordById(int id);
    }
}
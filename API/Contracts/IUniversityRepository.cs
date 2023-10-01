using API.Models;

namespace API.Contracts
{
    public interface IUniversityRepository
    {
        IEnumerable<University> GetAll();
        University? Create(University university);
        University? GetByGuid(Guid guid);
        bool Update(University university);
        bool Delete(University university);


    }
}

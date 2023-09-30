using API.Models;

namespace API.Contracts
{
    public interface IUniversityReposiroty
    {
        IEnumerable<University> GetAll();
        University? Create(University university);
        University? GetByGuid(Guid guid);
        bool Update(University university);
        bool Delete(University university);


    }
}

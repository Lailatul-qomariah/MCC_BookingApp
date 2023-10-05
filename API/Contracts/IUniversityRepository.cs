using API.Models;

namespace API.Contracts
{
    public interface IUniversityRepository : IGenericRepository<University>
    {
       public University GetCodeName(string code, string name);
    }
}

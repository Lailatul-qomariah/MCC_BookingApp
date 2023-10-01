using API.Models;

namespace API.Contracts
{
    public interface IAllRepository<T>
    {
        IEnumerable<T> GetAll();
        T? GetByGuid(Guid guid);
        T? Create(T T);
        bool Update(T T);
        bool Delete(T T);


    }
}

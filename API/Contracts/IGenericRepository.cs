using API.Models;

namespace API.Contracts
{
    //dibuat interface agar controller tidak berkomunikasi langsung dengan repositories
    public interface IGenericRepository<T> where T : class //interface generic.
    {
        IEnumerable<T> GetAll(); //getAll dengan model Generic
        T? GetByGuid(Guid guid); //get by guid dengan model dan parameter generic
        T? Create(T entitiy); //create dengan model dan parameter generic
        bool Update(T entitiy); //update dengan model dan parameter generic
        bool Delete(T entitiy); //delete dengan model dan parameter generic



    }
}

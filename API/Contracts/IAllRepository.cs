using API.Models;

namespace API.Contracts
{
    //dibuat interface agar controller tidak berkomunikasi langsung dengan repositories
    public interface IAllRepository<T> //interface generic.
    {
        IEnumerable<T> GetAll(); //getAll dengan model Generic
        T? GetByGuid(Guid guid); //get by guid dengan model dan parameter generic
        T? Create(T T); //create dengan model dan parameter generic
        bool Update(T T); //update dengan model dan parameter generic
        bool Delete(T T); //delete dengan model dan parameter generic


    }
}

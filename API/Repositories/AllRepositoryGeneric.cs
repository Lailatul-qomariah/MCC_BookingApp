using API.Contracts;
using API.Data;
using API.Utilities.Handlers;

namespace API.Repositories;
//repository generic dan inheritance dengan Interface genertic dan hanya menerima tipe generic berupa class
public class AllRepositoryGeneric<T> : IGenericRepository<T> where 
    T : class

{
    private readonly BookingManagementDBContext _context; //akses db context

    public AllRepositoryGeneric(BookingManagementDBContext context) //injection untuk instance db context
    {
        _context = context;
    }
    public T? Create(T entity) //metod untuk create data di db
    {
        try
        {
            _context.Set<T>().Add(entity); //add data ke db berdasarkan inputan atau isi var entityT
            _context.SaveChanges(); //sama kayak commit
            return entity; //return data yg ditambahkan
        }catch (Exception ex)
        {
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);

        }
    }

    public bool Delete(T entity) //method delete data dari model
    {
        try
        {
            _context.Set<T>().Remove(entity); //remove data dari db
            _context.SaveChanges(); //commit
            return true;
        }
        catch (Exception ex)
        {
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
        }
    }

    public IEnumerable<T> GetAll() //method get all data dari model tertentu
    {
        return _context.Set<T>().ToList(); //data diconvert ke bentuk list
    }

    public T? GetByGuid(Guid guid) //method untuk get data by guid
    {
        var entity = _context.Set<T>().Find(guid);
        _context.ChangeTracker.Clear();
        return entity;
    }

    public bool Update(T entity)
    {
        try
        {
            _context.Set<T>().Update(entity); //update data db berdasarkan model 
            _context.SaveChanges(); //commit
            return true;
        }
        catch (Exception ex) 
        {
            throw new ExceptionHandler(ex.InnerException?.Message ?? ex.Message);
        }
    }
}

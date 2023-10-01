using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories;
//repository generic dan inheritance dengan Interface genertic dan hanya menerima tipe generic berupa class
public class AllRepositoryGeneric<T> : IAllRepository<T> where T : class

{
    private readonly BookingManagementDBContext _context; //akses db context

    public AllRepositoryGeneric(BookingManagementDBContext context) //injection untuk instance db context
    {
        _context = context;
    }
    public T? Create(T entityT) //metod untuk create data di db
    {
        try
        {
            _context.Set<T>().Add(entityT); //add data ke db berdasarkan inputan atau isi var entityT
            _context.SaveChanges(); //sama kayak commit
            return entityT; //return data yg ditambahkan
        }catch
        {
            return null;
        }
    }

    public bool Delete(T entityT) //method delete data dari model
    {
        try
        {
            _context.Set<T>().Remove(entityT); //remove data dari db
            _context.SaveChanges(); //commit
            return true;
        }
        catch
        {
            return false;
        }
    }

    public IEnumerable<T> GetAll() //method get all data dari model tertentu
    {
        return _context.Set<T>().ToList(); //data diconvert ke bentuk list
    }

    public T? GetByGuid(Guid guid) //method untuk get data by guid
    {
        return _context.Set<T>().Find(guid); //return datanya berdasarkan guid yang ditemukan
    }

    public bool Update(T entityT)
    {
        try
        {
            _context.Set<T>().Update(entityT); //update data db berdasarkan model 
            _context.SaveChanges(); //commit
            return true;
        }
        catch
        {
            return false;
        }
    }
}

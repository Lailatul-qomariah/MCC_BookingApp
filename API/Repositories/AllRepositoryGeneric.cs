using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class AllRepositoryGeneric<T> : IAllRepository<T> where T : class

    {
        private readonly BookingManagementDBContext _context;

        public AllRepositoryGeneric(BookingManagementDBContext context)
        {
            _context = context;
        }
        public T? Create(T entityT)
        {
            try
            {
                _context.Set<T>().Add(entityT);
                _context.SaveChanges();
                return entityT;
            }catch
            {
                return null;
            }
        }

        public bool Delete(T entityT)
        {
            try
            {
                _context.Set<T>().Remove(entityT);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public T? GetByGuid(Guid guid)
        {
            return _context.Set<T>().Find(guid);
        }

        public bool Update(T entityT)
        {
            try
            {
                _context.Set<T>().Update(entityT);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}

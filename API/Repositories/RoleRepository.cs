using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoleRepository : IRoleRepository

    {
        private readonly BookingManagementDBContext _context;

        public RoleRepository(BookingManagementDBContext context)
        {
            _context = context;
        }
        public Role? Create(Role role)
        {
            try
            {
                _context.Set<Role>().Add(role);
                _context.SaveChanges();
                return role;
            }catch
            {
                return null;
            }
        }

        public bool Delete(Role role)
        {
            try
            {
                _context.Set<Role>().Remove(role);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Role> GetAll()
        {
            return _context.Set<Role>().ToList();
        }

        public Role? GetByGuid(Guid guid)
        {
            return _context.Set<Role>().Find(guid);
        }

        public bool Update(Role role)
        {
            try
            {
                _context.Set<Role>().Update(role);
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

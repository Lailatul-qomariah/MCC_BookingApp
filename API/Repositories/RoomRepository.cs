using API.Contracts;
using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repositories
{
    public class RoomRepository : IAllRepository<Room>

    {
        private readonly BookingManagementDBContext _context;

        public RoomRepository(BookingManagementDBContext context)
        {
            _context = context;
        }
        public Room? Create(Room room)
        {
            try
            {
                _context.Set<Room>().Add(room);
                _context.SaveChanges();
                return room;
            }catch
            {
                return null;
            }
        }

        public bool Delete(Room room)
        {
            try
            {
                _context.Set<Room>().Remove(room);
                _context.SaveChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public IEnumerable<Room> GetAll()
        {
            return _context.Set<Room>().ToList();
        }

        public Room? GetByGuid(Guid guid)
        {
            return _context.Set<Room>().Find(guid);
        }

        public bool Update(Room room)
        {
            try
            {
                _context.Set<Room>().Update(room);
                _context.SaveChanges();
                return true;
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error updating room: {ex.Message}");
                return false;
            }
        }
    }
}

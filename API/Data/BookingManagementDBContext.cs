using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Data
{
    public class BookingManagementDBContext : DbContext //db context bawaan dari entity framework
    {
        public BookingManagementDBContext(DbContextOptions<BookingManagementDBContext> options) : base (options) { }
       
        public DbSet<Account> Accounts { get; set; }
        public DbSet<AccountRole> AccountRoles { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Education> Educations { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<University> Universities { get; set; }

        //method untuk ngatur uniq type data 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Employee>().HasIndex(e => new 
            {
                e.Nik,
                e.Email,
                e.PhoneNumber
            }).IsUnique();
        }


    }
}

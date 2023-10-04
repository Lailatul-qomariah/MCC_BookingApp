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

            modelBuilder.Entity<Employee>().HasIndex(e => e.Nik).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.Email).IsUnique();
            modelBuilder.Entity<Employee>().HasIndex(e => e.PhoneNumber).IsUnique();

            //relasi university dan education
            modelBuilder.Entity<University>()
                    .HasMany(e => e.Education)
                    .WithOne(u => u.University)
                    .HasForeignKey(e => e.UniversityGuid)
                    .OnDelete(DeleteBehavior.Restrict);

            //relasi education dan employee
            modelBuilder.Entity<Education>()
                    .HasOne(e => e.Employee)
                    .WithOne(e => e.Education)
                    .HasForeignKey<Education>(e => e.Guid)
                    .OnDelete(DeleteBehavior.Restrict);

            //relasi employee dan account
            modelBuilder.Entity<Employee>()
                .HasOne(a => a.Account)
                .WithOne(e => e.Employee)
                .HasForeignKey<Account>(a => a.Guid)
                .OnDelete(DeleteBehavior.Restrict);

            //relasi account dan account role
            modelBuilder.Entity<Account>()
                .HasMany(ar => ar.AccountRole)
                .WithOne(a => a.Account)
                .HasForeignKey(ar => ar.AccountGuid)
                .OnDelete(DeleteBehavior.Restrict);

            //accountrole dan role
            modelBuilder.Entity<AccountRole>()
                .HasOne(r => r.Role)
                .WithMany(ar => ar.AccountRole)
                .HasForeignKey(ar => ar.RoleGuid)
                .OnDelete(DeleteBehavior.Restrict);

            //employee dan booking 
            modelBuilder.Entity<Employee>()
                .HasMany(b => b.Booking)
                .WithOne(e => e.Employee)
                .HasForeignKey(b => b.EmployeeGuid)
                .OnDelete(DeleteBehavior.Restrict);

            //booking dan room
            modelBuilder.Entity<Booking>()
                .HasOne(r => r.Room)
                .WithMany(b => b.Booking)
                .HasForeignKey(b => b.RoomGuid)
                .OnDelete(DeleteBehavior.Restrict);

        
        }

        //ondelete 
        


    }
}

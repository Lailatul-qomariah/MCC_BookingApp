using API.Contracts;
using API.Data;
using API.Models;
using API.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<BookingManagementDBContext>(option  => option.UseSqlServer(connectionString));

//add repositories to the container / GENERIC
/*builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();
builder.Services.AddScoped<IGenericRepository<Account>, AllRepositoryGeneric<Account>>();
builder.Services.AddScoped<IGenericRepository<AccountRole>, AllRepositoryGeneric<AccountRole>>();
builder.Services.AddScoped<IGenericRepository<Booking>, AllRepositoryGeneric<Booking>>();
builder.Services.AddScoped<IAllRepository<Education>, AllRepositoryGeneric<Education>>();
builder.Services.AddScoped<IGenericRepository<Employee>, AllRepositoryGeneric<Employee>>();
builder.Services.AddScoped<IGenericRepository<Role>, AllRepositoryGeneric<Role>>();
builder.Services.AddScoped<IGenericRepository<Room>, AllRepositoryGeneric<Room>>();
builder.Services.AddScoped<IAllRepository<University>, AllRepositoryGeneric<University>>();*/



//add repositories to the container / NON GENERIC
builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();
builder.Services.AddScoped<IUniversityRepository, UniversityRepository>();



builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

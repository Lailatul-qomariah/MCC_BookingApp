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
builder.Services.AddScoped<IAllRepository<Account>, AllRepositoryGeneric<Account>>();
builder.Services.AddScoped<IAllRepository<AccountRole>, AllRepositoryGeneric<AccountRole>>();
builder.Services.AddScoped<IAllRepository<Booking>, AllRepositoryGeneric<Booking>>();
/*builder.Services.AddScoped<IAllRepository<Education>, AllRepositoryGeneric<Education>>();
builder.Services.AddScoped<IAllRepository<Employee>, AllRepositoryGeneric<Employee>>();*/
builder.Services.AddScoped<IAllRepository<Role>, AllRepositoryGeneric<Role>>();
builder.Services.AddScoped<IAllRepository<Room>, AllRepositoryGeneric<Room>>();
//builder.Services.AddScoped<IAllRepository<University>, AllRepositoryGeneric<University>>();

//add repositories to the container / NON GENERIC
/*builder.Services.AddScoped<IAccountRepository, AccountRepository>();
builder.Services.AddScoped<IAccountRoleRepository, AccountRoleRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();*/
builder.Services.AddScoped<IEducationRepository, EducationRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
/*builder.Services.AddScoped<IRoleRepository, RoleRepository>();
builder.Services.AddScoped<IRoomRepository, RoomRepository>();*/
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

using API.Contracts;
using API.Data;
using API.Repositories;
using API.Utilities.Handlers;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

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



builder.Services.AddControllers()
       .ConfigureApiBehaviorOptions(options =>
       {
           // Custom validation response
           options.InvalidModelStateResponseFactory = context =>
           {
               var errors = context.ModelState.Values
                                   .SelectMany(v => v.Errors)
                                   .Select(v => v.ErrorMessage);
               //manggil error handler 400 bad request
               return new BadRequestObjectResult(new ResponseValidatorHandler(errors)); 
           };
       });

//ada di dokumentasi, untuk custom errornya 
builder.Services.AddFluentValidationAutoValidation()
    .AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

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

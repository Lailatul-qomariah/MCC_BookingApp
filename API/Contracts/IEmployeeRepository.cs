using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Contracts
{
    public interface IEmployeeRepository : IGenericRepository<Employee>
    {
        public Employee GetLastNik();
        public Employee GetByEmail(string email);

    }
}

using API.Models;

namespace API.Contracts
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        public Account GetByEmail(string email);

    }
}

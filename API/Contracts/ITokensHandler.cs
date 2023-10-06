using System.Security.Claims;

namespace API.Contracts
{
    public interface ITokensHandler
    {
        string  Generate(IEnumerable<Claim> claims);
    }
}

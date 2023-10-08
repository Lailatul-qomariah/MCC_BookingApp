using API.Contracts;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace API.Utilities.Handlers;

public class TokensHandler : ITokensHandler
{
    private readonly IConfiguration _configuration; //bawaan dari .net
    public TokensHandler(IConfiguration configuration) 
    {
        _configuration = configuration; 
    }
    public string Generate(IEnumerable<Claim> claims)
    {
        // Membuat objek SymmetricSecurityKey menggunakan SecretKey dari configuration
        var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWTService:SecretKey"]));
        // Membuat objek SigningCredentials menggunakan secretKey dan algoritma HmacSha256
        var sigingCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
        // Membuat objek JwtSecurityToken dengan mengisi parameter-parameter yang diperlukan
        var tokenOptions = new JwtSecurityToken(issuer: _configuration["JWTService:Issuer"],
            audience: _configuration["JWTService:Audience"],
            claims: claims,
            expires: DateTime.Now.AddMinutes(10), // Token akan kedaluwarsa dalam 10 menit.
            signingCredentials: sigingCredentials);

        // Encode token menggunakan JwtSecurityTokenHandler
        var encodedToken = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
        return encodedToken; // Return token yang telah di-generate
    }
}

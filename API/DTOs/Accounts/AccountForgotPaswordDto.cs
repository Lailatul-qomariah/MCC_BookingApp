using API.Models;

namespace API.DTOs.Accounts
{
    public class AccountForgotPaswordDto
    {
        public int Otp { get; set; }
        public DateTime ExpireTime { get; set; }
    }


 
}

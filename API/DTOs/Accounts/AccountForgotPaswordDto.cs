using API.Models;

namespace API.DTOs.Accounts
{
    public class AccountForgotPaswordDto
    {
        //properti forgot password yang akan ditampilkan di view user
        public int Otp { get; set; }
        public DateTime ExpireTime { get; set; }
    }


 
}

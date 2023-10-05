namespace API.DTOs.Accounts
{
    public class AccountChangePasswordDto
    {
        public string Email { get; set; }
        public int Otp { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }



    }
}

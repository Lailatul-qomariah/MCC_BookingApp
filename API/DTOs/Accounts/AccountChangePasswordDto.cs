namespace API.DTOs.Accounts
{
    public class AccountChangePasswordDto
    {
        //properti change password yang akan jadi inputan user
        public string Email { get; set; }
        public int Otp { get; set; }
        public string ConfirmPassword { get; set; }
        public string NewPassword { get; set; }



    }
}

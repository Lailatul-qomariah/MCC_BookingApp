namespace API.Utilities.Handlers
{
    public class HashHandler
    {
        private static string GEtRandomSalt()
        {
            // Generate salt acak dengan panjang 12 karakter.
            return BCrypt.Net.BCrypt.GenerateSalt(12);
        }

        public static string HashPassword(string password)
        {
            //Melakukan hash password dengan salt acak.
            return BCrypt.Net.BCrypt.HashPassword(password, GEtRandomSalt());
        }

        // memverifikasi password dengan hashed yang ada.
        //untuk login nanti
        public static bool VerifyPassword(string password, string hashedPassword)
        {
            return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
        }
    }
}

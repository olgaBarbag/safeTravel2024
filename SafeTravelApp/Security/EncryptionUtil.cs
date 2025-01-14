
namespace SchoolApp.Security
{
    public static class EncryptionUtil
    {
        public static string Encrypt(string rawPass)
        {
            var encryptedPass = BCrypt.Net.BCrypt.HashPassword(rawPass);
            return encryptedPass;
        }

        public static bool IsValidPassword(string plainText, string cipherText)
        {
            return BCrypt.Net.BCrypt.Verify(plainText, cipherText);
        }
    }
}

using BC = BCrypt.Net.BCrypt;

namespace AuthenticationAPI.Application.Adapters;

public static class CryptographyAdapter
{
    public static void Verify(string password, string encryptedPassword)
    {
        bool samePassword = BC.Verify(password, encryptedPassword);

        if (samePassword == false)
            throw new Exception("Invalid password.");
    }

    public static string HashPassword(string password)
    {
        return BC.HashPassword(password, 12);
    }
}

using System.Security.Cryptography;

namespace Jobinja.Services;

public static class PasswordHashService
{
    public static string HashPassword(string password)
    {
        byte[] salt = RandomNumberGenerator.GetBytes(16);
        int iterations = 600000;

        byte[] hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            iterations,
            HashAlgorithmName.SHA256,
            32
        );

        return $"$pbkdf2-sha256${iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    public static bool VerifyPassword(string password, string storedHash)
    {
        string[] parts = storedHash.Split('$');

        if (parts.Length != 5 || parts[1] != "pbkdf2-sha256")
            return false;

        if (!int.TryParse(parts[2], out int iterations))
            return false;

        try
        {
            byte[] salt = Convert.FromBase64String(parts[3]);
            byte[] originalHash = Convert.FromBase64String(parts[4]);

            byte[] enteredHash = Rfc2898DeriveBytes.Pbkdf2(
                password,
                salt,
                iterations,
                HashAlgorithmName.SHA256,
                originalHash.Length
            );

            return CryptographicOperations.FixedTimeEquals(
                enteredHash,
                originalHash
            );
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
using System.Security.Cryptography;

namespace UserCRUD.Application.Common.Utils.Common;

public static class StringGenerator
{
    public static string GenerateToken(int length)
    {
        var randomNumber = new byte[length];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomNumber);
        return Convert.ToBase64String(randomNumber);
    }
}

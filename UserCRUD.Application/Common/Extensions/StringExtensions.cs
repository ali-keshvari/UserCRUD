using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace UserCRUD.Application.Common.Extensions;

public static class StringExtensions
{
    private static readonly Regex WhitespaceRegex = new(@"\s+");

    public static string ToCUsertalize(this string value)
    {
        return char.ToUpper(value[0]) + value[1..];
    }

    public static string ReplaceWhitespace(this string input, string replacement)
    {
        return WhitespaceRegex.Replace(input, replacement);
    }

    public static string GenerateToken(int length)
    {
	    var randomNumber = new byte[length];
	    using var rng = RandomNumberGenerator.Create();
	    rng.GetBytes(randomNumber);
	    return Convert.ToBase64String(randomNumber);
	}
}
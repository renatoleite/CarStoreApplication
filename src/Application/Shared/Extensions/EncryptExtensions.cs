using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography;
using System.Text;

namespace Application.Shared.Extensions
{
    [ExcludeFromCodeCoverage]
    public static class EncryptExtensions
    {
        public static string CreateSHA256Hash(this string input)
        {
            var inputBytes = Encoding.UTF8.GetBytes(input);
            var inputHash = SHA256.HashData(inputBytes);
            return Convert.ToHexString(inputHash).ToLower();
        }
    }
}

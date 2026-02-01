using System.Security.Cryptography;
using System.Text;

namespace RentApi.Extensions;

public static class StringExtensions
{
  public static string ToSha256(this string rawData)
  {
    if (string.IsNullOrEmpty(rawData)) return string.Empty;

    // 1. Algoritmni yaratamiz
    using var sha256 = SHA256.Create();

    // 2. Stringni baytlarga o'giramiz
    var bytes = Encoding.UTF8.GetBytes(rawData);

    // 3. Hashlaymiz
    var hash = sha256.ComputeHash(bytes);

    // 4. HexString (64 ta belgi) ga o'giramiz
    return Convert.ToHexString(hash);
  }
}

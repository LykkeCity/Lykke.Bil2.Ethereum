using System.Text;
using JetBrains.Annotations;

namespace Lykke.Bil2.Ethereum.Utils
{
    [PublicAPI]
    public static class StringExtensions
    {
        public static string ToHex(
            this string str)
        {
            return Encoding.UTF8.GetBytes(str).ToHexString();
        }
    }
}

using System.Text;
using System.Text.RegularExpressions;

namespace Lykke.Bil2.Ethereum.Utils
{
    public static class StringUtil
    {
        public static readonly System.Text.Encoding UTF8 = new UTF8Encoding(false, false);

        static readonly Regex NewLineRegex = new Regex(@"\r\n|\n\r|\n|\r");

        public static string NormalizeNewLines(string input, string newLine = "\n")
        {
            return NewLineRegex.Replace(input, newLine);
        }

    }
}

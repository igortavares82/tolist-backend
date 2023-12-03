using System.Globalization;
using System.Text.RegularExpressions;
using System.Web;
using Standard.ToList.Model.Constants;

namespace Standard.ToList.Infrastructure.Extensions
{
    public static class StringExtension
	{
        public static string ToStr(this string input) 
        {
            if (string.IsNullOrEmpty(input) || Regex.IsMatch(input, RegexPatterns.SEARCHER_OPEN_SYMBOLS))
                return string.Empty;

            input = input.Replace("\n", string.Empty);
            input = HttpUtility.HtmlDecode(input);
            input = string.Concat(char.ToUpper(input[0]), input.Substring(1, input.Length - 1));

            return input;
        }

        public static decimal ToDecimal(this string input, string culture = "pt-PT")
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            return decimal.Parse(input, new CultureInfo(culture));
        }

        public static string ToPadLeft(this string input, int length)
        {
            if (string.IsNullOrEmpty(input) || !int.TryParse(input, out int number))
                return string.Empty;

            return input.PadLeft(length, '0');
        }
    }
}


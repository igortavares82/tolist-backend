﻿using System.Globalization;
using System.Web;

namespace Standard.ToList.Infrastructure.Extensions
{
    public static class StringExtension
	{
        public static string Cleanup(this string input) 
        {
            if (string.IsNullOrEmpty(input))
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
    }
}

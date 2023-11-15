using System;
using System.Globalization;
using System.Text;
using System.Web;

namespace Standard.ToList.Application.Extensions
{
    public static class StringExtension
	{
		public static string ToHash(this string input, string salt = "")
		{
            if (string.IsNullOrEmpty(input))
            {
                return null;
            }

            using (var sha = new System.Security.Cryptography.SHA256Managed())
            {
                byte[] textBytes = Encoding.UTF8.GetBytes(input + salt);
                byte[] hashBytes = sha.ComputeHash(textBytes);

                string hash = BitConverter.ToString(hashBytes)
                                          .Replace("-", string.Empty);

                return hash;
            }
        }

        public static string GetDateMask(this string input) => "yyyy-MM-dd HH:mm:ss";

        public static string SetMessageValues(this string input, params string[] values) => string.Format(input, values);

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


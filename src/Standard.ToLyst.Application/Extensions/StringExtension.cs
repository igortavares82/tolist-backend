using System;
using System.Text;

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
    }
}


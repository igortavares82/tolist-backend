namespace Standard.ToLyst.Api.Extensions
{
    public static class IHeaderDictionaryExtension
    {
		public static string ExtractToken(this IHeaderDictionary input)
		{
            string token = string.Empty;

            if (input.ContainsKey("Authorization"))
            {
                token = input["Authorization"].ToString().Replace("Bearer ", string.Empty);
            }

            return token;
        }
	}
}


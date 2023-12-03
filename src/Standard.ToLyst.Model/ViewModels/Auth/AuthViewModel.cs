using System;

namespace Standard.ToLyst.Model.ViewModels.Auth
{
	public class AuthViewModel
	{
		public AuthViewModel(string token, DateTime expires)
		{
			Token = token;
			Expires = expires.ToString();
		}

        public string Token { get; private set; }
		public string Expires { get; private set; }
	}
}


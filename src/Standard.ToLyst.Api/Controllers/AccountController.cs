using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Standard.ToLyst.Model.Aggregates.Users;
using Standard.ToLyst.Model.Options;

namespace Standard.ToLyst.Api.Controllers
{

    [AllowAnonymous]
	[Route("account")]
	public class AccountController : Controller
    {
        private readonly SignInManager<User> _signInManager;
        private readonly AppSettingOptions _settings;

        public AccountController(/*SignInManager<User> signInManager,*/ IOptions<AppSettingOptions> settings)
        {
            //_signInManager = signInManager;
            _settings = settings.Value;
        }

        [HttpPost("extenal-login")]
		public IActionResult ExternalLogin(string provider, string returnUrl)
		{
			var redirectUrl = $"{_settings.BackendUrl}/identity/account/external-auth-callback?returnUrl={returnUrl}";
			var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
			properties.AllowRefresh = true;

			return Challenge(properties, provider);
		}

		[HttpGet("external-auth-callback")]
		public async Task<IActionResult> ExternalLoginCallback()
		{
			var info = await _signInManager.GetExternalLoginInfoAsync();
			//var result = await

			var options = new CookieOptions
			{
				Domain = "",
                Expires = DateTime.UtcNow.AddMinutes(5)
            };

			Response.Cookies.Append(
				"",
                JsonConvert.SerializeObject(null, new JsonSerializerSettings
                {
                    ContractResolver = new DefaultContractResolver
                    {
                        NamingStrategy = new CamelCaseNamingStrategy()
                    },
                    Formatting = Formatting.Indented
                }),
				options
			);

            return Redirect($"{_settings.FrontendUrl}/auth/external-login-callback-front-end-page");
        }
    }
}

using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Standard.ToList.Model.Aggregates.Users;
using Standard.ToList.Model.Options;

namespace Standard.ToList.Application.Services
{
	public class TokenService
	{
		private readonly AppSettingOptions _settings;

        public TokenService(IOptions<AppSettingOptions> settings)
		{
			_settings = settings.Value;
		}

		public string GetToken(User user)
		{
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SecurityToken);
			var claims = new Claim[]
			{
				new Claim(ClaimTypes.Sid, user.Id),
				new Claim(ClaimTypes.Name, user.Name),
				new Claim(ClaimTypes.Role, user.Role.ToString()),
			};

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(2),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Aes128CbcHmacSha256)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
        }
	}
}


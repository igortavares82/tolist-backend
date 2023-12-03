using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
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

		public string GetToken(User user, double expirationInDays = 300)
		{
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_settings.SecurityToken);
			var claims = GetClaims(user);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(claims),
				Expires = DateTime.UtcNow.AddDays(expirationInDays),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.Aes128CbcHmacSha256)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
        }

		public string[] GetValues(string token, params string[] @params) 
		{
			var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
			var values = new List<string>();

			foreach (var param in @params) 
			{
				var value = jwt.Claims.FirstOrDefault(it => it.Type == ClaimTypes.Sid)?.Value;
				
				if (string.IsNullOrEmpty(value))
					continue;

				values.Add(value); 
			}

            return values.ToArray();
		}

		public bool IsValid(string token)
		{
			var jwt = new JwtSecurityTokenHandler().ReadJwtToken(token);
			return jwt.ValidTo > DateTime.UtcNow;
		}

		private Claim[] GetClaims(User user)
		{
			List<Claim> claims = new List<Claim>()
			{
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role.ToString())
            };

			if (!user.Id.IsNullOrEmpty())
				claims.Add(new Claim(ClaimTypes.Sid, user.Id));

			return claims.ToArray();
        }
	}
}


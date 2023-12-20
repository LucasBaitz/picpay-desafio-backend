using BankChallenge.Application.Services.Interfaces;
using BankChallenge.Domain.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace BankChallenge.Application.Services
{
	public class TokenGeneratorService : ITokenGeneratorService
	{
		private readonly SymmetricSecurityKey _securityKey;

		public TokenGeneratorService()
		{
			_securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("TH3@B357!53CUR17Y--K3Y$%3VERM4DEF0RSUR3"));
		}

		public string GenerateToken(User user)
		{

			List<Claim> claims = new List<Claim>
			{
				new Claim("id", user.Id),
				new Claim("username", user.UserName!),
				new Claim("email", user.Email!),
				new Claim("loginTimeStamp", DateTime.UtcNow.ToString())
			};

			var signingCredentials = new SigningCredentials(_securityKey, SecurityAlgorithms.HmacSha256);

			var token = new JwtSecurityToken
				(
				expires: DateTime.Now.AddHours(6),
				claims: claims,
				signingCredentials: signingCredentials
				);

			return new JwtSecurityTokenHandler().WriteToken(token);
		}
	}
}

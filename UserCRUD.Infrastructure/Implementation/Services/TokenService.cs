using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Models.Common.SiteSetting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace UserCRUD.Infrastructure.Implementation.Services;

public class TokenService : ITokenService
{
	private readonly SiteSettings _siteSettings;
	private readonly IHttpContextAccessor _httpContextAccessor;

	public TokenService(
        IOptionsSnapshot<SiteSettings> siteSettings,
        IHttpContextAccessor httpContextAccessor)
	{
		_siteSettings = siteSettings.Value;
		_httpContextAccessor = httpContextAccessor;
	}

	public JwtSecurityToken CreateAccessToken(List<Claim>? authClaims)
	{
		var authSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSettings.Jwt.SecurityKey));

		var token = new JwtSecurityToken(
			issuer: _siteSettings.Jwt.ValidIssuer,
			audience: _httpContextAccessor.HttpContext?.Request.Headers.Referer,
			expires: DateTime.Now.AddMinutes(_siteSettings.Jwt.TokenValidityInMinutes),
			claims: authClaims,
			signingCredentials: new SigningCredentials(authSigningKey, SecurityAlgorithms.HmacSha256)
			);

		return token;
	}

	public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
	{
		var tokenValidationParameters = new TokenValidationParameters
		{
			ValidateAudience = false,
			ValidateIssuer = false,
			ValidateIssuerSigningKey = true,
			IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_siteSettings.Jwt.SecurityKey)),
			ValidateLifetime = false
		};

		var tokenHandler = new JwtSecurityTokenHandler();
		var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
		if (securityToken is not JwtSecurityToken jwtSecurityToken ||
			!jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase))
			throw new SecurityTokenException("Invalid token");

		return principal;
	}

	public bool ValidateToken(string token)
	{
		if (string.IsNullOrEmpty(token))
			return false;

		var key = Encoding.UTF8.GetBytes(_siteSettings.Jwt.SecurityKey);

		try
		{
			new JwtSecurityTokenHandler()
				.ValidateToken(token, new TokenValidationParameters
				{
					ValidateIssuerSigningKey = true,
					IssuerSigningKey = new SymmetricSecurityKey(key),
					ValidateIssuer = false,
					ValidateAudience = false,
					ClockSkew = TimeSpan.Zero
				}, out SecurityToken validatedToken);

			var jwtToken = (JwtSecurityToken)validatedToken;

			return true;
		}
		catch
		{
			return false;
		}
	}
}
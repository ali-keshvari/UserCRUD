using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace UserCRUD.Application.Contracts.Services;

public interface ITokenService
{
	JwtSecurityToken CreateAccessToken(List<Claim>? authClaims);
	ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
}
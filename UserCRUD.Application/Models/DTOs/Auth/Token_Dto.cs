namespace UserCRUD.Application.Models.DTOs.Auth;

public class Token_Dto
{
    public string? AccessToken { get; set; }
    public DateTime? AccessTokenExpiryDateTime { get; set; }
    public string? RefreshToken { get; set; }
    public DateTime? RefreshTokenExpiryDateTime { get; set; }
}
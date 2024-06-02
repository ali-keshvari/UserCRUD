using UserCRUD.Domain.Enum;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace UserCRUD.Application.Models.Common.SiteSetting;

public class SiteSettings
{
    public JwtSettings Jwt { get; set; } = null!;
    public List<UserSeed> DefaultUsers { get; set; } = new();
    public SmsSettings Sms { get; set; } = null!;
    public SmtpSettings Smtp { get; set; } = null!;
    public ConnectionStrings ConnectionStrings { get; set; } = null!;
    public TimeSpan EmailConfirmationTokenProviderLifespan { get; set; }
    public PasswordOptions PasswordOptions { get; set; } = null!;
    public ActiveDatabaseEnum ActiveDatabase { get; set; }
    public CookieOptions CookieOptions { get; set; } = null!;
    public DataProtectionOptions DataProtectionOptions { get; set; } = null!;
    public LockoutOptions LockoutOptions { get; set; } = null!;
}
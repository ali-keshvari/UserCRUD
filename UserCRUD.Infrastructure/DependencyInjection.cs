using System.Reflection;
using UserCRUD.Application.Contracts.Data;
using UserCRUD.Application.Contracts.Persistence;
using UserCRUD.Application.Contracts.Persistence.Identity;
using UserCRUD.Application.Contracts.Services;
using UserCRUD.Application.Models.Common.SiteSetting;
using UserCRUD.Infrastructure.Data;
using UserCRUD.Infrastructure.Data.Context;
using UserCRUD.Infrastructure.Implementation.Repositories;
using UserCRUD.Infrastructure.Implementation.Repositories.Identity;
using UserCRUD.Infrastructure.Implementation.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Configuration;
using UserCRUD.Application.Contracts.Persistence.Common;
using UserCRUD.Infrastructure.Implementation.Common;

namespace UserCRUD.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var siteSettings = services.BuildServiceProvider()
            .GetRequiredService<IOptionsSnapshot<SiteSettings>>().Value;

        var assembly = Assembly.GetExecutingAssembly().GetName().Name;
        var connectionString = configuration.GetValue<string>("ConnectionStrings:SqlServer");

        services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        //services.AddIdentity<User, Role>()
        //    .AddEntityFrameworkStores<AppDbContext>()
        //    .AddDefaultTokenProviders();

        //services.AddAuthentication(options =>
        //{
        //    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        //}).AddJwtBearer(options =>
        //{
        //    options.SaveToken = true;
        //    options.RequireHttpsMetadata = false;
        //    options.TokenValidationParameters = new TokenValidationParameters()
        //    {
        //        ValidateIssuer = true,
        //        ValidateAudience = true,
        //        ValidateLifetime = true,
        //        ValidateIssuerSigningKey = true,
        //        ClockSkew = TimeSpan.Zero,
        //        ValidAudiences = siteSettings.Jwt.ValidAudiences,
        //        ValidIssuer = siteSettings.Jwt.ValidIssuer,
        //        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(siteSettings.Jwt.SecurityKey))
        //    };
        //});



        services.AddScoped<IDatabaseInitializer, DatabaseInitializer>();

        // Repositories
        services.AddScoped(typeof(IRepositoryBase<,>), typeof(RepositoryBase<,>));
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IFileRepository, FileRepository>();

        // Services
        services.AddScoped<ITokenService, TokenService>();
        services.AddScoped<IHttpService, HttpService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IVerifyNationalCodeService, VerifyNationalCodeService>();
        services.AddScoped<IReportService, ReportService>();
        services.AddScoped<IValidFilesService, ValidFilesService>();

        return services;
    }
}
using UserCRUD.Application.Common.Exceptions;
using UserCRUD.Application.Contracts.Data;
using Microsoft.Extensions.DependencyInjection;

namespace UserCRUD.Infrastructure.Data.Extensions;

public static class AppDbContextExtensions
{
    public static async Task InitializeDatabaseAsync(this IServiceCollection services)
    {
        var databaseInitializer = services.BuildServiceProvider()
            .GetRequiredService<IDatabaseInitializer>();

        try
        {
            databaseInitializer.Initialize();
            await databaseInitializer.SeedRoleDataAsync();
            await databaseInitializer.SeedUserDataAsync();
        }
        catch (Exception ex)
        {
            throw new ContextException("User | Role", ex);
        }
    }
} 

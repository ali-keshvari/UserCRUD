namespace UserCRUD.Application.Contracts.Data;

public interface IDatabaseInitializer
{
    void Initialize();
    Task SeedRoleDataAsync();
    Task SeedUserDataAsync();
}

namespace UserCRUD.Application.Contracts.Data;

public interface IAppDbContext
{
	// TODO: Add db set properties here

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}
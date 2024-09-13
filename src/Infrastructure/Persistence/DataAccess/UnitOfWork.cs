using Application.Contracts.Persistence;

namespace Persistence.DataAccess;

public sealed class UnitOfWork(AppDbContext appDbContext) : IUnitOfWork
{
    public IDatabaseTransaction BeginTransaction()
    {
        return new DatabaseTransaction(appDbContext.Database.BeginTransaction());
    }

    public async Task SaveChangesAsync()
    {
        await appDbContext.SaveChangesAsync();
    }
}

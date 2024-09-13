using Application.Contracts.Persistence;
using Microsoft.EntityFrameworkCore.Storage;

namespace Persistence.DataAccess;

public sealed class DatabaseTransaction(IDbContextTransaction dbContextTransaction) : IDatabaseTransaction
{
    public void Commit()
    {
        dbContextTransaction.Commit();
    }

    public void Dispose()
    {
        dbContextTransaction.Dispose();
    }

    public void Rollback()
    {
        dbContextTransaction.Rollback();
    }
}

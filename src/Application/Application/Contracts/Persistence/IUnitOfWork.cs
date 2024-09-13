namespace Application.Contracts.Persistence;

public interface IUnitOfWork
{
    IDatabaseTransaction BeginTransaction();
    Task SaveChangesAsync();
}

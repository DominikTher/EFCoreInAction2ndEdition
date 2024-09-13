namespace Application.Contracts.Persistence;

public interface IDatabaseTransaction : IDisposable
{
    public void Commit();
    public void Rollback();
}

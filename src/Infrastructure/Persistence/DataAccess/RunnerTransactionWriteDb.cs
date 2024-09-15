using Application.Contracts.Persistence;
using Application.Core;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Persistence.DataAccess;

// Unit of Work with save changes and transaction
// Might be a collection of actions, depends on scenario
public class RunnerTransactionWriteDb<TIn, TPass, TOut>(
    AppDbContext appDbContext,
    IBizAction<TIn, TPass> actionPart1,
    IBizAction<TPass, TOut> actionPart2) : IRunnerTransactionWriteDb<TIn, TOut>
        where TPass : class, new()
        where TOut : class, new()
{
    public IImmutableList<ValidationResult> Errors { get; private set; } = [];

    public bool HasErrors => Errors.Any();

    public async Task<TOut> RunAction(TIn dataIn)
    {
        // Try-Catch instead of errors also possible
        using var transaction = appDbContext.Database.BeginTransaction();
        var passResult = await RunPart(actionPart1, dataIn);
        if (HasErrors)
        {
            // TODO Return value
            return default!;
        }

        var result = await RunPart(actionPart2, passResult);
        if (!HasErrors)
        {
            transaction.Commit();
        }

        return result;
    }

    private async Task<TPartOut> RunPart<TPartIn, TPartOut>(
        IBizAction<TPartIn, TPartOut> bizPart,
        TPartIn dataIn)
        where TPartOut : class, new()
    {
        var result = bizPart.Action(dataIn);
        Errors = bizPart.Errors;
        if (!HasErrors)
        {
            await appDbContext.SaveChangesAsync();
        }

        return result;
    }
}

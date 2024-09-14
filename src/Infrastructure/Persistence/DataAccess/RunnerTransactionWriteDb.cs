using Application.Core;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Persistence.DataAccess;

// Unit of Work with save changes and transaction
// Might be a collection of actions, depends on scenario
public class RunnerTransactionWriteDb<TIn, TPass, TOut>(
    AppDbContext appDbContext,
    IBizAction<TIn, TPass> actionPart1,
    IBizAction<TPass, TOut> actionPart2)
       where TPass : class
       where TOut : class
{
    public IImmutableList<ValidationResult> Errors { get; private set; } = [];

    public bool HasErrors => Errors.Any();

    public TOut RunAction(TIn dataIn)
    {
        using var transaction = appDbContext.Database.BeginTransaction();
        var passResult = RunPart(actionPart1, dataIn);
        if (HasErrors)
        {
            return null;
        }

        var result = RunPart(actionPart2, passResult);
        if (!HasErrors)
        {
            transaction.Commit();
        }

        return result;
    }

    private TPartOut RunPart<TPartIn, TPartOut>(
        IBizAction<TPartIn, TPartOut> bizPart,
        TPartIn dataIn)
        where TPartOut : class
    {
        var result = bizPart.Action(dataIn);
        Errors = bizPart.Errors;
        if (!HasErrors)
        {
            appDbContext.SaveChanges();
        }

        return result;
    }
}

using Application.Contracts.Persistence;
using Application.Core;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Persistence.DataAccess;

// UoW
public sealed class RunnerWriteDb<TIn, TOut>(
    IBizAction<TIn, TOut> actionClass,
    AppDbContext context) : IRunnerWriteDbAsync<TIn, TOut>
{
    public IImmutableList<ValidationResult> Errors => actionClass.Errors;
    public bool HasErrors => actionClass.HasErrors;

    public async Task<TOut> RunAction(TIn dataIn)
    {
        var result = actionClass.Action(dataIn);
        if (!HasErrors)
            await context.SaveChangesAsync();

        return result;
    }
}

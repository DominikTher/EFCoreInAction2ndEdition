using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Persistence;

public interface IRunnerTransactionWriteDb<TIn, TOut> where TOut : class, new()
{
    IImmutableList<ValidationResult> Errors { get; }
    bool HasErrors { get; }
    Task<TOut> RunAction(TIn dataIn);
}

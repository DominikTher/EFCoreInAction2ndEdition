using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Persistence;

public interface IRunnerWriteDbAsync<TIn, TOut>
{
    IImmutableList<ValidationResult> Errors { get; }
    bool HasErrors { get; }
    Task<TOut> RunAction(TIn dataIn);
}

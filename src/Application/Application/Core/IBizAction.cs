using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;

namespace Application.Core;

public interface IBizAction<in TIn, out TOut>
{
    IImmutableList<ValidationResult> Errors { get; }
    bool HasErrors { get; }
    TOut Action(TIn dto);
}

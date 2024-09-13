using Application.Core;

namespace Application.Contracts.Persistence.Queries;

public sealed record SimpleBookQuery
{
    public OrderByOptions OrderBy { get; set; }
    public BooksFilterBy FilterBy { get; set; }
    public string FilterValue { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

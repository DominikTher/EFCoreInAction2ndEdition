using Application.Core;

namespace Api.Models;

public sealed record GetBooks
{
    public OrderByOptions OrderBy { get; set; }
    public BooksFilterBy FilterBy { get; set; }
    public string FilterValue { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}

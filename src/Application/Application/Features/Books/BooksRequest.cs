using Application.Core;
using MediatR;

namespace Application.Features.Books;

public sealed record BooksRequest : IRequest<IEnumerable<BooksOutput>>
{
    public OrderByOptions OrderBy { get; set; }
    public BooksFilterBy FilterBy { get; set; }
    public string FilterValue { get; set; } = string.Empty;
    public int PageNumber { get; set; }
    public int PageSize { get; set; }
}
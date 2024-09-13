using Application.Contracts.Persistence.Queries;
using Mapster;
using MediatR;

namespace Application.Features.Books;

public sealed class BooksRequestHandler(ISimpleBookQueryHandler simpleBookSelect) : IRequestHandler<BooksRequest, IEnumerable<BooksOutput>>
{
    private readonly ISimpleBookQueryHandler simpleBookSelect = simpleBookSelect;

    public async Task<IEnumerable<BooksOutput>> Handle(BooksRequest request, CancellationToken cancellationToken)
    {
        var books = await simpleBookSelect.Get(request.Adapt<SimpleBookQuery>(), cancellationToken);

        return books.Adapt<IEnumerable<BooksOutput>>();
    }
}

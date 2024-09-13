using Application.Core;

namespace Application.Contracts.Persistence.Queries;

public interface ISimpleBookQueryHandler
{
    public Task<IEnumerable<BookListDto>> Get(SimpleBookQuery simpleBookQuery, CancellationToken cancellationToken);
}

using Application.Contracts.Persistence.Queries;
using Application.Core;
using Microsoft.EntityFrameworkCore;
using Persistence.DataAccess;
using Persistence.QueryObjects;
using Persistence.QueryObjects.Books;

namespace Persistence.Books;

public sealed class SimpleBookQueryHandler(IDbContextFactory<AppDbContext> dbContextFactory) : ISimpleBookQueryHandler
{
    private readonly IDbContextFactory<AppDbContext> dbContextFactory = dbContextFactory;

    public async Task<IEnumerable<BookListDto>> Get(SimpleBookQuery simpleBookQuery, CancellationToken cancellationToken)
    {
        using var dbContext = await dbContextFactory.CreateDbContextAsync(cancellationToken);

        var query = dbContext
            .Books
            .MapBookToDto()
            .OrderBooksBy(simpleBookQuery.OrderBy)
            .FilterBooksBy(simpleBookQuery.FilterBy, simpleBookQuery.FilterValue);

        //simpleBookQuery.SetupRestOfDto(query, cancellationToken);

        query = await query.Page(simpleBookQuery.PageNumber, simpleBookQuery.PageSize, cancellationToken);
        return await query.ToListAsync(cancellationToken);
    }
}

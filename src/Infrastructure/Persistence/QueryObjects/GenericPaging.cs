using Microsoft.EntityFrameworkCore;

namespace Persistence.QueryObjects;

public static class GenericPaging
{
    public static async Task<IQueryable<T>> Page<T>(this IQueryable<T> query, int pageNumber, int pageSize, CancellationToken cancellationToken)
    {
        var numberOfPages = (int)Math.Ceiling((double)await query.CountAsync(cancellationToken) / pageSize);
        pageNumber = Math.Min(Math.Max(1, pageNumber), numberOfPages);

        return query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);
    }
}

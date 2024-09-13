using Application.Core;

namespace Persistence.QueryObjects.Books;

public static class BookListDtoSort
{
    public static IQueryable<BookListDto> OrderBooksBy(this IQueryable<BookListDto> books, OrderByOptions orderByOptions)
        => orderByOptions switch
        {
            OrderByOptions.SimpleOrder => books.OrderByDescending(x => x.BookId),
            OrderByOptions.ByVotes => books.OrderByDescending(x => x.ReviewsAverageVotes),
            OrderByOptions.ByPublicationDate => books.OrderByDescending(x => x.PublishedOn),
            OrderByOptions.ByPriceLowestFirst => books.OrderBy(x => x.ActualPrice),
            OrderByOptions.ByPriceHighestFirst => books.OrderByDescending(x => x.ActualPrice),
            _ => throw new ArgumentOutOfRangeException(nameof(orderByOptions), orderByOptions, null),
        };
}

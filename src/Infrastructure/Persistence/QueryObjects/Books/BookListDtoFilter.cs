using Application.Core;
using Microsoft.EntityFrameworkCore;

namespace Persistence.QueryObjects.Books;

public static class BookListDtoFilter
{
    public static IQueryable<BookListDto> FilterBooksBy(this IQueryable<BookListDto> books, BooksFilterBy filterBy, string filterValue)
    {
        if (string.IsNullOrEmpty(filterValue))
            return books;

        switch (filterBy)
        {
            case BooksFilterBy.NoFilter:
                return books;
            case BooksFilterBy.ByVotes:
                var filterVote = int.Parse(filterValue);
                return books.Where(x => x.ReviewsAverageVotes > filterVote);
            case BooksFilterBy.ByTags:
                return books.Where(x => x.TagStrings.Any(y => y == filterValue));
            case BooksFilterBy.Title:
                //return books.Where(x => x.Title.Contains(filterValue));
                return books.Where(x => EF.Functions.Like(EF.Functions.Collate(x.Title, "Latin1_General_CI_AS"), $"_______ {filterValue}"));
            default:
                throw new ArgumentOutOfRangeException(nameof(filterBy), filterBy, null);
        }
    }
}

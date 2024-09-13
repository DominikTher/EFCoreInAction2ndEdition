using Application.Contracts.Persistence.Models;
using Application.Core;

namespace Persistence.QueryObjects.Books;

public static class BookListDtoSelect
{
    public static IQueryable<BookListDto> MapBookToDto(this IQueryable<Book> books)
        => books.Select(book => new BookListDto
        {
            BookId = book.BookId,
            Title = book.Title,
            Price = book.Price,
            PublishedOn = book.PublishedOn,
            ActualPrice = book.Promotion == null
                ? book.Price
                : book.Promotion.NewPrice,
            PromotionPromotionalText =
            book.Promotion == null
                ? string.Empty
                : book.Promotion.PromotionalText,
            AuthorsOrdered = string.Join(", ",
                book
                    .AuthorsLink
                    .OrderBy(ba => ba.Order)
                    .Select(ba => ba.Author.Name)),
            ReviewsCount = book.Reviews.Count,
            ReviewsAverageVotes =
                book
                    .Reviews
                    .Select(review => (double?)review.NumStars)
                    .Average(),
            TagStrings = book.Tags.Select(x => x.TagId).ToArray(),
        });
}

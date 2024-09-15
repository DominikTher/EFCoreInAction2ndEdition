using Application.Contracts.Persistence.DbAccess;
using Application.Contracts.Persistence.Errors;
using Application.Contracts.Persistence.Models;
using Application.Core;

namespace Application.Features.Orders.PlaceOrder;

public sealed class PlaceLineItemsAction(IPlaceOrderDbAccess placeOrderDbAccess) : BizActionErrors, IBizAction<PlaceLineItemsQuery, Order>
{
    public Order Action(PlaceLineItemsQuery placeLineItemsQuery)
    {
        var booksDict = placeOrderDbAccess.FindBooksByIdsWithPriceOffers(placeLineItemsQuery.LineItems.Select(x => x.BookId));
        placeLineItemsQuery.Order.LineItems = FormLineItemsWithErrorChecking(placeLineItemsQuery.LineItems, booksDict);

        return HasErrors ? new EmptyOrder() : placeLineItemsQuery.Order;
    }

    private List<LineItem> FormLineItemsWithErrorChecking(IEnumerable<OrderLineItem> lineItems, IDictionary<int, Book> booksDict)
    {
        var result = new List<LineItem>();

        foreach (var (index, lineItem) in lineItems.Index()) // This new method is crazy :)
        {
            if (!booksDict.TryGetValue(lineItem.BookId, out Book? book))
            {
                throw new InvalidOperationException($"An order failed because book, id = {lineItem.BookId} was missing.");
            }

            var bookPrice = book.Promotion?.NewPrice ?? book.Price;
            if (bookPrice <= 0)
            {
                AddError($"Sorry, the book '{book.Title}' is not for sale.");
            }
            else
            {
                result.Add(new LineItem
                {
                    BookPrice = bookPrice,
                    ChosenBook = book,
                    LineNum = (byte)(index + 1),
                    NumBooks = lineItem.NumBooks
                });
            }
        }
        return result;
    }
}

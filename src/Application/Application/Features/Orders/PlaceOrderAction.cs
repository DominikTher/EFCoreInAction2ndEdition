using Application.Contracts.Persistence.DbAccess;
using Application.Contracts.Persistence.Errors;
using Application.Contracts.Persistence.Models;
using Application.Core;

namespace Application.Features.Orders;

public sealed class PlaceOrderAction(IPlaceOrderDbAccess placeOrderDbAccess) : BizActionErrors, IBizAction<PlaceOrderInDto, Order>
{
    public Order Action(PlaceOrderInDto placeOrderInDto)
    {
        if (!placeOrderInDto.AcceptTAndCs)
        {
            AddError("You must accept the T&Cs to place an order.");
            return new EmptyOrder();
        }

        if (!placeOrderInDto.LineItems.Any())
        {
            AddError("No items in your basket.");
            return new EmptyOrder();
        }

        var booksDict = placeOrderDbAccess.FindBooksByIdsWithPriceOffers(placeOrderInDto.LineItems.Select(x => x.BookId));
        var order = new Order
        {
            CustomerId = placeOrderInDto.UserId,
            LineItems = FormLineItemsWithErrorChecking(placeOrderInDto.LineItems, booksDict)
        };

        if (!HasErrors)
        {
            placeOrderDbAccess.Add(order);
        }

        return HasErrors ? new EmptyOrder() : order;
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

using Application.Contracts.Persistence.DbAccess;
using Application.Contracts.Persistence.Errors;
using Application.Contracts.Persistence.Models;
using Application.Core;

namespace Application.Features.Orders.PlaceOrder;

public sealed class PlaceOrderAction(IPlaceOrderDbAccess placeOrderDbAccess) : BizActionErrors, IBizAction<PlaceOrder2InDto, PlaceLineItemsQuery>
{
    public PlaceLineItemsQuery Action(PlaceOrder2InDto placeOrderInDto)
    {
        var booksDict = placeOrderDbAccess.FindBooksByIdsWithPriceOffers(placeOrderInDto.LineItems.Select(x => x.BookId));
        var order = new Order
        {
            CustomerId = placeOrderInDto.UserId,
        };

        if (!HasErrors)
        {
            placeOrderDbAccess.Add(order);
        }

        return new PlaceLineItemsQuery { Order = order, LineItems = placeOrderInDto.LineItems };
    }
}

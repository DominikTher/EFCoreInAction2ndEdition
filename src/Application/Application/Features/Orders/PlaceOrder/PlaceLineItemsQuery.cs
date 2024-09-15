using Application.Contracts.Persistence.Models;
using System.Collections.Immutable;

namespace Application.Features.Orders.PlaceOrder;

public sealed class PlaceLineItemsQuery
{
    public IImmutableList<OrderLineItem> LineItems { get; set; } = [];
    public Order Order { get; set; } = new EmptyOrder();
}

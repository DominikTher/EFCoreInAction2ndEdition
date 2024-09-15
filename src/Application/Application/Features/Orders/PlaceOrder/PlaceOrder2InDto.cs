using System.Collections.Immutable;

namespace Application.Features.Orders;

public sealed record PlaceOrder2InDto
{
    public PlaceOrder2InDto(bool acceptTAndCs, Guid userId, IImmutableList<OrderLineItem> lineItems)
    {
        AcceptTAndCs = acceptTAndCs;
        UserId = userId;
        LineItems = lineItems;
    }

    public bool AcceptTAndCs { get; private set; }

    public Guid UserId { get; private set; }

    public IImmutableList<OrderLineItem> LineItems { get; private set; }
}

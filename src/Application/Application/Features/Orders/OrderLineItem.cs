namespace Application.Features.Orders;

public sealed record OrderLineItem
{
    public int BookId { get; set; }

    public short NumBooks { get; set; }
}
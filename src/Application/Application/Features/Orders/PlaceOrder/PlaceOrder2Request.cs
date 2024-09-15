using MediatR;

namespace Application.Features.Orders.PlaceOrder;

public sealed record PlaceOrder2Request : IRequest<PlaceOrderOutput>
{
}
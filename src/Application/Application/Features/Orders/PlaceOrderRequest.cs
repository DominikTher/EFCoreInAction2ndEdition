using MediatR;

namespace Application.Features.Orders;

public sealed record PlaceOrderRequest : IRequest<PlaceOrderOutput>
{
}
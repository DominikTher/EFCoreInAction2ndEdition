using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Models;
using MediatR;
using System.Collections.Immutable;

namespace Application.Features.Orders;

public sealed class PlaceOrderRequestHandler(IRunnerWriteDbAsync<PlaceOrderInDto, Order> placeOrderAction)
    : IRequestHandler<PlaceOrderRequest, PlaceOrderOutput>
{
    public async Task<PlaceOrderOutput> Handle(PlaceOrderRequest request, CancellationToken cancellationToken)
    {
        // TODO Parameters should be simplified
        var placeOrderInDto = new PlaceOrderInDto(
            true,
            Guid.CreateVersion7(),
            ImmutableList.Create(new OrderLineItem { BookId = 1, NumBooks = 1 }));

        var order = await placeOrderAction.RunAction(placeOrderInDto);

        if (placeOrderAction.HasErrors)
        {
            throw new ApplicationException(string.Join(",", placeOrderAction.Errors.Select(error => error.ErrorMessage)));
        }

        return new PlaceOrderOutput();
    }
}

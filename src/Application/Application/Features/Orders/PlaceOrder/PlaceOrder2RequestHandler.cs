using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Models;
using MediatR;
using System.Collections.Immutable;

namespace Application.Features.Orders.PlaceOrder;

// This is just showcase of part 2 with UoW and multiple save changes
public sealed class PlaceOrder2RequestHandler(IRunnerTransactionWriteDb<PlaceOrder2InDto, Order> placeOrderAction)
    : IRequestHandler<PlaceOrder2Request, PlaceOrderOutput>
{
    public async Task<PlaceOrderOutput> Handle(PlaceOrder2Request request, CancellationToken cancellationToken)
    {
        // TODO Parameters should be simplified
        var placeOrderInDto = new PlaceOrder2InDto(
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

using Application.Features.Orders;
using Application.Features.Orders.PlaceOrder;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class OrderController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public Task<PlaceOrderOutput> Get(CancellationToken cancellationToken)
    {
        return mediator.Send(new PlaceOrderRequest(), cancellationToken);
    }

    [HttpGet("part-two")]
    public Task<PlaceOrderOutput> GetPartTwo(CancellationToken cancellationToken)
    {
        return mediator.Send(new PlaceOrder2Request(), cancellationToken);
    }
}

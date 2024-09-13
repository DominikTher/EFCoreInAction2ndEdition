using Api.Models;
using Application.Features.Books;
using Mapster;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public Task<IEnumerable<BooksOutput>> Get([FromQuery] GetBooks getBooks, CancellationToken cancellationToken)
    {
        return mediator.Send(getBooks.Adapt<BooksRequest>(), cancellationToken);
    }
}

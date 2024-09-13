using Application.Contracts.Persistence.DbAccess;
using Application.Contracts.Persistence.Models;
using Microsoft.EntityFrameworkCore;
using Persistence.DataAccess;

namespace Persistence.DbAccess;

public sealed class PlaceOrderDbAccess(AppDbContext appDbContext) : IPlaceOrderDbAccess
{
    public void Add(Order newOrder)
    {
        appDbContext.Add(newOrder);
    }

    public IDictionary<int, Book> FindBooksByIdsWithPriceOffers(IEnumerable<int> bookIds)
    {
        return appDbContext
            .Books
            .AsTracking()
            .Where(x => bookIds.Contains(x.BookId))
            .Include(r => r.Promotion)
            .ToDictionary(key => key.BookId);
    }
}
using Application.Contracts.Persistence.Models;

namespace Application.Contracts.Persistence.DbAccess;

public interface IPlaceOrderDbAccess
{
    IDictionary<int, Book> FindBooksByIdsWithPriceOffers(IEnumerable<int> bookIds);

    void Add(Order newOrder);
}

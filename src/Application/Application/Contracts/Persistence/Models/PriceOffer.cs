namespace Application.Contracts.Persistence.Models;

public class PriceOffer
{
    public int PriceOfferId { get; set; }
    public decimal NewPrice { get; set; }
    public string PromotionalText { get; set; } = string.Empty;

    public int BookId { get; set; }
}

using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Persistence.Models;

#nullable disable

public class LineItem
{
    public int LineItemId { get; set; }

    [Range(1, 5, ErrorMessage = "This order is over the limit of 5 books.")]
    public byte LineNum { get; set; }

    public short NumBooks { get; set; }
    public decimal BookPrice { get; set; }

    public int OrderId { get; set; }
    public int BookId { get; set; }

    public Book ChosenBook { get; set; }
}
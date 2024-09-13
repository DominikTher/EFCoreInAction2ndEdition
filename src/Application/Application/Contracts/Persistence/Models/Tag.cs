using System.ComponentModel.DataAnnotations;

namespace Application.Contracts.Persistence.Models;

public class Tag
{
    [Key]
    [Required]
    [MaxLength(40)]
    public string TagId { get; set; } = string.Empty;

    public ICollection<Book> Books { get; set; } = [];
}

namespace Application.Contracts.Persistence.Models;

public class Review
{
    public int ReviewId { get; set; }
    public string VoterName { get; set; } = string.Empty;
    public int NumStars { get; set; }
    public string Comment { get; set; } = string.Empty;

    public int BookId { get; set; }
}

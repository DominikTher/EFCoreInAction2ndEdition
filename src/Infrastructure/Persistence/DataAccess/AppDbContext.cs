using Application.Contracts.Persistence;
using Application.Contracts.Persistence.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistence.DataAccess;

public sealed class AppDbContext(DbContextOptions<AppDbContext> dbContextOptions) : DbContext(dbContextOptions), IAppDbContext
{
    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Tag> Tags { get; set; }
    public DbSet<PriceOffer> PriceOffers { get; set; }
    public DbSet<Order> Orders { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<BookAuthor>()
            .HasKey(x => new { x.BookId, x.AuthorId });

        modelBuilder
            .Entity<LineItem>()
            .HasOne(p => p.ChosenBook)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict); // Test it

        modelBuilder
            .Entity<Book>()
            .HasQueryFilter(p => !p.SoftDeleted);

        modelBuilder
            .Entity<Author>()
            .HasData(new Author
            {
                AuthorId = 1,
                Name = "Martin Fowler",
                WebUrl = "http://martinfowler.com/"
            });

        modelBuilder
            .Entity<Book>()
            .HasData([
                new Book
                {
                    BookId = 1,
                    Title = "Refactoring",
                    Description = "Improving the design of existing code",
                    PublishedOn = new DateTime(1999, 7, 8)
                },
                new Book
                {
                    BookId = 2,
                    Title = "Patterns of Enterprise Application Architecture",
                    Description = "Written in direct response to the stiff challenges",
                    PublishedOn = new DateTime(2002, 11, 15)
                },
                new Book
                {
                    BookId = 3,
                    Title = "Domain-Driven Design",
                    Description = "Linking business needs to software design",
                    PublishedOn = new DateTime(2003, 8, 30)
                },
                new Book
                {
                    BookId = 4,
                    Title = "Quantum Networking",
                    Description = "Entangled quantum networking provides faster-than-light data communications",
                    PublishedOn = new DateTime(2057, 1, 1)
                }
            ]);

        modelBuilder
            .Entity<BookAuthor>()
            .HasData([
                new BookAuthor { AuthorId = 1, BookId = 1, Order = 1 },
                new BookAuthor { AuthorId = 1, BookId = 2, Order = 1 },
                new BookAuthor { AuthorId = 1, BookId = 3, Order = 1 },
                new BookAuthor { AuthorId = 1, BookId = 4, Order = 1 }
            ]);
    }
}

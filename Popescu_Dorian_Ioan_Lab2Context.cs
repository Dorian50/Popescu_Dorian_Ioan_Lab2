using Microsoft.EntityFrameworkCore;
using Popescu_Dorian_Ioan_Lab2.Models;

public class Popescu_Dorian_Ioan_Lab2Context(DbContextOptions<Popescu_Dorian_Ioan_Lab2Context> options) : DbContext(options)
{
    public DbSet<Popescu_Dorian_Ioan_Lab2.Models.Book> Book { get; set; } = default!;
    public DbSet<Publisher> Publisher { get; set; }
    public DbSet<Author> Author { get; set; }

    public DbSet<Category> Category { get; set; }
    public DbSet<BookCategory> BookCategory { get; set; }
}

using Microsoft.EntityFrameworkCore;
using PlaywrightSandbox.Web.Models;

namespace PlaywrightSandbox.Web.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Game> Games => Set<Game>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Game>().HasData(
            new Game { Id = 1, Title = "Street Fighter II", Genre = "Fighting", ReleaseYear = 1991, Rating = 9.5, Description = "The iconic 1v1 fighter that defined the genre." },
            new Game { Id = 2, Title = "Mortal Kombat", Genre = "Fighting", ReleaseYear = 1992, Rating = 9.0, Description = "Brutally realistic fighter famous for its fatalities." },
            new Game { Id = 3, Title = "Art of Fighting", Genre = "Fighting", ReleaseYear = 1992, Rating = 8.5, Description = "SNK's power-gauge fighter that introduced super moves." }
        );
    }
}

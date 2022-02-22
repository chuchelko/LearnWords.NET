using LearnWords.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LearnWords.DAL.Plugins;

public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Word> Words { get; set; }
    private readonly ILogger<AppDbContext> _logger;
    public AppDbContext(DbContextOptions<AppDbContext> dbContextOptions, ILogger<AppDbContext> logger) : base(dbContextOptions)
    {
        _logger = logger;
        Database.EnsureCreated();
    }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.LogTo(Console.WriteLine);
        optionsBuilder.EnableSensitiveDataLogging();
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = "Azat",
            Age = 22,
            NativeLanguage = Language.Russian
        };
        var words = new List<Word>();
        words.Add(new Word { UserId = user.Id, WordFrom = "Disco", WordTo = "Диско", From = Language.English, To = Language.Russian });
        words.Add(new Word { UserId = user.Id, WordFrom = "Чай", WordTo = "Tea", From = Language.Russian, To = Language.English });
        words.Add(new Word { UserId = user.Id, WordFrom = "Coffee", WordTo = "Кофе", From = Language.English, To = Language.Russian });
        


        modelBuilder.Entity<User>().HasData(user);
        modelBuilder.Entity<Word>().HasData(words);
        base.OnModelCreating(modelBuilder);
    }
}

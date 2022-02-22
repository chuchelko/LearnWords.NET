
using LearnWords.DAL.Entities;
using LearnWords.DAL.Interfaces;
using LearnWords.DAL.Plugins;
using Microsoft.EntityFrameworkCore;

namespace LearnWords.DAL.Core;

public class CoreRepository : ICoreRepository
{
    private readonly AppDbContext _context;
    public IRepository<User> Users { get; }
    public IRepository<Word> Words { get; }

    public CoreRepository(IRepository<User> Users, IRepository<Word> Words, AppDbContext context)
    {
        this.Users = Users;
        this.Words = Words;
        _context = context;
        _context.Database.EnsureCreated();
    }

    public IEnumerable<Word> GetUsersWords(Guid userid, Language FirstLang, Language SecondLang)
    {

        var words1 = _context
            .Words
            .Where(word => word.From == FirstLang && word.To == SecondLang || word.To == FirstLang && word.From == SecondLang)
            .AsNoTracking()
            ?? throw new ArgumentException("Words or User not found");
        return words1;
    }

    public User GetFirstUser()
    {
        return _context.Users.First();
    }
}

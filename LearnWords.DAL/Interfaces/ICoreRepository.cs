using LearnWords.DAL.Entities;

namespace LearnWords.DAL.Interfaces;

public interface ICoreRepository
{
    public IRepository<User> Users { get; }
    public IRepository<Word> Words { get; }
    public IEnumerable<Word> GetUsersWords(Guid userid, Language FirstLang, Language SecondLang);
    public User GetFirstUser();
}

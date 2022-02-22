namespace LearnWords.DAL.Entities;

public class User : UniqueWithId
{
    public int? Age { get; set; }
    public string Name { get; set; } = Guid.NewGuid().ToString();
    public string? Email { get; set; }
    public Language NativeLanguage { get; set; }
    public int WordsCount { get; set; } = 0;
    public List<Word> Words { get; set; } = new List<Word>();
}


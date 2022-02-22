using LearnWords.DAL.Entities;

namespace LearnWords.Services.Models;

public class UserDTO
{
    public Guid Id { get; set; }
    public int? Age { get; set; }
    public string Name { get; set; } = Guid.NewGuid().ToString();
    public string? Email { get; set; }
    public Language NativeLanguage { get; set; }
    public int WordsCount { get; set; } = 0;
}

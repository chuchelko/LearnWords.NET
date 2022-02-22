using LearnWords.DAL.Entities;

namespace LearnWords.Services.Models;

public class WordDTO
{
    public Language From { get; set; }
    public Language To { get; set; }
    public string WordFrom { get; set; }
    public string WordTo { get; set; }
    public string? Description { get; set; }

}

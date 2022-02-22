namespace LearnWords.DAL.Entities;
public class Word : UniqueWithId
{
    public Guid UserId { get; init; }
    public User User { get; init; }
    public Language From { get; set; }
    public Language To { get; set; }
    public string WordFrom { get; set; }
    public string WordTo { get; set; }
    public string? Description { get; set; }

}

public enum Language
{
    Russian,
    English,
    Spanish,
    Chinese
}
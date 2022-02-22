namespace LearnWords.DAL;

public class UniqueWithId
{
    public Guid Id { get; init; } = Guid.NewGuid();
}
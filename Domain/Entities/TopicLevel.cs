namespace Domain.Entities;

public class TopicLevel
{
    public int TopicId { get; set; }
    public Topic Topic { get; set; }
    public int LevelId { get; set; }
    public Level Level { get; set; }
}

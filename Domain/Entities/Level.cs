namespace Domain.Entities;

public class Level
{
    public Level(string title, string color)
    {
        Title = title;
        Color = color;
    }

    public int Id { get; set; }
    public string Title { get; set; }
    public string Color { get; set; }

    public ICollection<TopicLevel> TopicLevels { get; set; }
}

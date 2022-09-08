namespace Domain.Entities;

public class Topic
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }

    public ICollection<TopicLevel> TopicLevels { get; set; }
    public ICollection<TopicTag> TopicTags { get; set; }
}
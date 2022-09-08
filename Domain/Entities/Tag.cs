namespace Domain.Entities;

public class Tag
{
    public Tag(string title)
    {
        Title = title;
    }

    public int Id { get; set; }
    public string Title { get; set; }

    public ICollection<TopicTag> TopicTags { get; set; }
}
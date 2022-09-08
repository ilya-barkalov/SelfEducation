namespace Domain.Entities;

public class TopicTag
{
    public int TopicId { get; set; }
    public Topic Topic { get; set; }
    public int TagId { get; set; }
    public Tag Tag { get; set; }
}

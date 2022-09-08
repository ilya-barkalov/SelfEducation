using Application.Common;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.GetTopic;

public class TopicDto
{
    public TopicDto(Topic topic)
    {
        Id = topic.Id;
        Title = topic.Title;
        Content = topic.Content;
        Tags = topic.TopicTags.Select(x => x.Tag.Title);
        Levels = topic.TopicLevels.Select(x => new LevelDto(x.Level.Title, x.Level.Color));
    }

    public int Id { get; }
    public string Title { get; }
    public string Content { get; }
    public IEnumerable<string> Tags { get; }
    public IEnumerable<LevelDto> Levels { get; }

    public class LevelDto
    {
        public LevelDto(string title, string color)
        {
            Title = title;
            Color = color;
        }

        public string Title { get; set; }
        public string Color { get; set; }
    }
}

public record GetTopicQuery(int Id) : IRequest<TopicDto>;

internal class GetTopicQueryHandler : IRequestHandler<GetTopicQuery, TopicDto>
{
    private readonly IApplicationDbContext _context;

    public GetTopicQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<TopicDto> Handle(GetTopicQuery request, CancellationToken cancellationToken)
    {
        var topic = await _context.Topics.AsNoTracking()
            .Include(x => x.TopicTags)
                .ThenInclude(x => x.Tag)
            .Include(x => x.TopicLevels)
                .ThenInclude(x => x.Level)
            .FirstOrDefaultAsync(t => t.Id == request.Id, cancellationToken);
        
        if (topic is null)
        {
            // TODO: Create own type exc eption for not found case.
            throw new Exception("Not found exception!");
        }

        return new TopicDto(topic);
    }
}
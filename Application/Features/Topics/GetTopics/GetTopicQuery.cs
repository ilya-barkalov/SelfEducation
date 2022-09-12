using Application.Common;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.Topics.GetTopics;

public class TopicDto
{
    public TopicDto(Topic topic)
    {
        Id = topic.Id;
        Title = topic.Title;
        Tags = topic.TopicTags.Select(x => x.Tag.Title);
        Levels = topic.TopicLevels.Select(x => new LevelDto(x.Level.Title, x.Level.Color));
    }

    public int Id { get; }
    public string Title { get; }
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

public record GetTopicsQuery(string Title, IEnumerable<int> Tags, IEnumerable<int> Levels) : IRequest<IEnumerable<TopicDto>>;

internal class GetTopicsQueryHandler : IRequestHandler<GetTopicsQuery, IEnumerable<TopicDto>>
{
    private readonly IApplicationDbContext _context;

    public GetTopicsQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<TopicDto>> Handle(GetTopicsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Topics.AsNoTracking()
            .Include(x => x.TopicTags)
                .ThenInclude(x => x.Tag)
            .Include(x => x.TopicLevels)
                .ThenInclude(x => x.Level)
            .AsQueryable();

        if (!string.IsNullOrWhiteSpace(request.Title))
        {
            query = query.Where(x => x.Title.Contains(request.Title));
        }

        if (request.Tags?.Any() == true)
        {
            query = query.Where(x
                => x.TopicTags.Any(y
                    => request.Tags.Contains(y.TagId)));
        }

        if (request.Levels?.Any() == true)
        {
            query = query.Where(x
                => x.TopicLevels.Any(y
                    => request.Levels.Contains(y.LevelId)));
        }

        var result = await query.Select(x => new TopicDto(x)).ToListAsync(cancellationToken);

        return result;
    }
}
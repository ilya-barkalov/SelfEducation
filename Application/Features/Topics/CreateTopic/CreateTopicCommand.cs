using Application.Common.Interfaces;
using Domain.Entities;

using MediatR;

namespace Application.Features.Topics.CreateTopic;

public record CreateTopicCommand : IRequest<int>
{
    public string Title { get; set; }
    public string Content { get; set; }
    public IEnumerable<int> Tags { get; set; }
    public IEnumerable<int> Levels { get; set; }
}

internal class CreateTopicCommandHandler : IRequestHandler<CreateTopicCommand, int>
{
    private readonly IApplicationDbContext _context;
    public CreateTopicCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<int> Handle(CreateTopicCommand request, CancellationToken cancellationToken)
    {
        var topic = new Topic
        {
            Title = request.Title,
            Content = request.Content,
            TopicTags = request.Tags.Select(x => new TopicTag { TagId = x }).ToList(),
            TopicLevels = request.Levels.Select(x => new TopicLevel { LevelId = x }).ToList()
        };

        await _context.Topics.AddAsync(topic, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return topic.Id;
    }
}
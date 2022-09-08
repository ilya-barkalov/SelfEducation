using Application.Common;

using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.UpdateTopic;

public class UpdateTopicCommand : IRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public List<int> Tags { get; set; } = new();
    public List<int> Levels { get; set; } = new();
}

internal class UpdateTopicCommandHandler : IRequestHandler<UpdateTopicCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTopicCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Unit> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        var topic = await _context.Topics
            .Include(x => x.TopicTags)
            .Include(x => x.TopicLevels)
            .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (topic is null)
        {
            // TODO: Create own type exc eption for not found case.
            throw new Exception("Not found exception!");
        }

        topic.Title = request.Title;
        topic.Content = request.Content;

        ProcessTags(topic, request.Tags);
        ProcessLevels(topic, request.Levels);

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }

    private void ProcessTags(Topic topic, IEnumerable<int> tags)
    {
        var tagsInTopic = topic.TopicTags.Select(x => x.TagId);
        
        foreach (var tag in tags)
        {
            var itemExists = tagsInTopic.Contains(tag);
            if (itemExists)
            {
                continue;
            }
            
            topic.TopicTags.Add(new TopicTag { TagId = tag, TopicId = topic.Id });
        }

        tagsInTopic = topic.TopicTags.Select(x => x.TagId);

        foreach (var tag in tagsInTopic)
        {
            var itemExists = tags.Contains(tag);
            if (itemExists)
            {
                continue;
            }

            var topicTag = topic.TopicTags.First(x => x.TagId == tag);

            topic.TopicTags.Remove(topicTag);
        }
    }

    private void ProcessLevels(Topic topic, IEnumerable<int> levels)
    {
        var levelsInTopic = topic.TopicLevels.Select(x => x.LevelId);

        foreach (var level in levels)
        {
            var itemExists = levelsInTopic.Contains(level);
            if (itemExists)
            {
                continue;
            }

            topic.TopicLevels.Add(new TopicLevel { LevelId = level, TopicId = topic.Id });
        }

        levelsInTopic = topic.TopicLevels.Select(x => x.LevelId);

        foreach (var level in levelsInTopic)
        {
            var itemExists = levels.Contains(level);
            if (itemExists)
            {
                continue;
            }

            var topicLevel = topic.TopicLevels.First(x => x.LevelId == level);

            topic.TopicLevels.Remove(topicLevel);
        }
    }
}

using Application.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.UpdateTopic;

public class UpdateTopicCommand : IRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public IEnumerable<int> Tags { get; set; }
    public IEnumerable<int> Levels { get; set; }
}

internal class UpdateTopicCommandHandler : IRequestHandler<UpdateTopicCommand, Unit>
{
    private readonly IApplicationDbContext _context;

    public UpdateTopicCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Unit> Handle(UpdateTopicCommand request, CancellationToken cancellationToken)
    {
        var topic = await _context.Topics.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (topic is null)
        {
            // TODO: Create own type exc eption for not found case.
            throw new Exception("Not found exception!");
        }

        topic.Title = request.Title;
        topic.Content = request.Content;

        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}

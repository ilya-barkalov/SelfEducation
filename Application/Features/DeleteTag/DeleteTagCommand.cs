using Application.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.DeleteTag;

public record DeleteTagCommand(int Id) : IRequest;

internal class DeleteTagCommandHandler : IRequestHandler<DeleteTagCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public DeleteTagCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Unit> Handle(DeleteTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (tag is null)
        {
            // TODO: Create own type exc eption for not found case.
            throw new Exception("Not found exception!");
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
using Application.Common.Exceptions;
using Application.Common.Interfaces;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.DeleteTag;

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
            throw new NotFoundException("Tag not found!");
        }

        _context.Tags.Remove(tag);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
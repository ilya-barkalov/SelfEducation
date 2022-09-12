using Application.Common.Interfaces;
using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.UpdateTag;

public record UpdateTagCommand(int Id, string Title) : IRequest;

internal class UpdateTagCommandHandler : IRequestHandler<UpdateTagCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public UpdateTagCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Unit> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);
        if (tag is null)
        {
            // TODO: Create own type exc eption for not found case.
            throw new Exception("Not found exception!");
        }

        tag.Title = request.Title;
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
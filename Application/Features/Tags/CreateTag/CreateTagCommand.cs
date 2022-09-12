using Application.Common.Interfaces;
using Domain.Entities;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.CreateTag;

public record CreateTagCommand(string Title) : IRequest;

internal class CreateTagCommandHandler : IRequestHandler<CreateTagCommand, Unit>
{
    private readonly IApplicationDbContext _context;
    public CreateTagCommandHandler(IApplicationDbContext context) => _context = context;

    public async Task<Unit> Handle(CreateTagCommand request, CancellationToken cancellationToken)
    {
        var alreadyExists = await _context.Tags.AnyAsync(x => x.Title == request.Title, cancellationToken);
        if (alreadyExists)
        {
            // TODO: Create own type exc eption for business logic validation.
            throw new Exception("Business logic exception!");
        }

        await _context.Tags.AddAsync(new Tag(request.Title), cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
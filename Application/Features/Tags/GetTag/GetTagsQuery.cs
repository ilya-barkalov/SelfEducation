using Application.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.GetTag;

public class TagDto
{
    public TagDto(int id, string title)
    {
        Id = id;
        Title = title;
    }

    public int Id { get; set; }
    public string Title { get; set; }
}

public record GetTagQuery(int Id) : IRequest<TagDto>;

internal class GetTagQueryHandler : IRequestHandler<GetTagQuery, TagDto>
{
    private readonly IApplicationDbContext _context;
    public GetTagQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<TagDto> Handle(GetTagQuery request, CancellationToken cancellationToken)
    {
        var tag = await _context.Tags
            .Where(x => x.Id == request.Id)
            .AsNoTracking()
            .Select(x => new TagDto(x.Id, x.Title))
            .FirstOrDefaultAsync(cancellationToken);

        if (tag is null)
        {
            // TODO: Create own type exc eption for not found case.
            throw new Exception("Not found exception!");
        }

        return tag;
    }
}
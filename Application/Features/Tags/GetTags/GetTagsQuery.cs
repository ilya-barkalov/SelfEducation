using Application.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.GetTags;

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

public record GetTagsQuery : IRequest<IEnumerable<TagDto>>;

internal class GetTagsQueryHandler : IRequestHandler<GetTagsQuery, IEnumerable<TagDto>>
{
    private readonly IApplicationDbContext _context;
    public GetTagsQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<TagDto>> Handle(GetTagsQuery request, CancellationToken cancellationToken)
    {
        var tags = await _context.Tags
            .AsNoTracking()
            .Select(x => new TagDto(x.Id, x.Title))
            .ToListAsync(cancellationToken);

        return tags;
    }
}
using Application.Common;

using MediatR;

using Microsoft.EntityFrameworkCore;

namespace Application.Features.Levels.GetLevels;

public class LevelDto
{
    public int Id { get; set; }
    public string Title { get; set; }
}

public record GetLevelsQuery : IRequest<IEnumerable<LevelDto>>;

internal class GetLevelsQueryHandler : IRequestHandler<GetLevelsQuery, IEnumerable<LevelDto>>
{
    private readonly IApplicationDbContext _context;
    public GetLevelsQueryHandler(IApplicationDbContext context) => _context = context;

    public async Task<IEnumerable<LevelDto>> Handle(GetLevelsQuery request, CancellationToken cancellationToken)
    {
        var levels = await _context.Levels
            .AsNoTracking()
            .OrderBy(x => x.Id)
            .Select(x => new LevelDto
            {
                Id = x.Id,
                Title = x.Title
            })
            .ToListAsync(cancellationToken);

        return levels;
    }
}

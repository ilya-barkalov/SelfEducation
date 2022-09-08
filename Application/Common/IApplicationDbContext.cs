using Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace Application.Common;

public interface IApplicationDbContext
{
    DbSet<Topic> Topics { get; }
    DbSet<Level> Levels { get; }
    DbSet<Tag> Tags { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}

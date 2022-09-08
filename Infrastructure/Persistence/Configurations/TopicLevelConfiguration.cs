using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class TopicLevelConfiguration : IEntityTypeConfiguration<TopicLevel>
{
    public void Configure(EntityTypeBuilder<TopicLevel> builder)
    {
        builder.ToTable(nameof(TopicLevel));

        builder.HasKey(e => new { e.TopicId, e.LevelId });

        builder.HasOne(e => e.Topic)
            .WithMany(e => e.TopicLevels)
            .HasForeignKey(e => e.TopicId);

        builder.HasOne(e => e.Level)
            .WithMany(e => e.TopicLevels)
            .HasForeignKey(e => e.LevelId);
    }
}

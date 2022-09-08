using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class TopicTagConfiguration : IEntityTypeConfiguration<TopicTag>
{
    public void Configure(EntityTypeBuilder<TopicTag> builder)
    {
        builder.ToTable(nameof(TopicTag));

        builder.HasKey(e => new { e.TopicId, e.TagId });

        builder.HasOne(e => e.Topic)
            .WithMany(e => e.TopicTags)
            .HasForeignKey(e => e.TopicId);

        builder.HasOne(e => e.Tag)
            .WithMany(e => e.TopicTags)
            .HasForeignKey(e => e.TagId);
    }
}

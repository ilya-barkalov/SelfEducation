using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class TopicConfiguration : IEntityTypeConfiguration<Topic>
{
    public void Configure(EntityTypeBuilder<Topic> builder)
    {
        builder.ToTable(nameof(Topic));

        builder.HasKey(e => e.Id);
        
        builder.Property(e => e.Title)
            .HasColumnName(nameof(Topic.Title))
            .HasMaxLength(240)
            .IsRequired(required: true);

        builder.Property(e => e.Content)
            .HasColumnName(nameof(Topic.Content))
            .IsRequired(required: true);

        builder.Property(e => e.Title)
            .HasColumnName(nameof(Topic.Title))
            .HasMaxLength(240)
            .IsRequired(required: true);
    }
}
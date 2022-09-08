using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class TagConfiguration : IEntityTypeConfiguration<Tag>
{
    public void Configure(EntityTypeBuilder<Tag> builder)
    {
        builder.ToTable(nameof(Tag));

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasColumnName(nameof(Tag.Title))
            .HasMaxLength(150)
            .IsRequired(required: true);

        var data = new List<Tag>
        {
            new(title: "C#") { Id = 1 },
            new(title: "JavaScript") { Id = 2 },
            new(title: "SQL") { Id = 3 },
        };

        builder.HasData(data);
    }
}

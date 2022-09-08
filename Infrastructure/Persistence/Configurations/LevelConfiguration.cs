using Domain.Entities;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Persistence.Configurations;

internal class LevelConfiguration : IEntityTypeConfiguration<Level>
{
    public void Configure(EntityTypeBuilder<Level> builder)
    {
        builder.ToTable(nameof(Level));

        builder.HasKey(e => e.Id);

        builder.Property(e => e.Title)
            .HasColumnName(nameof(Level.Title))
            .HasMaxLength(30)
            .IsRequired(required: true);

        builder.Property(e => e.Color)
            .HasColumnName(nameof(Level.Color))
            .HasMaxLength(7)
            .IsRequired(required: true);

        var data = new List<Level>
        {
            new(color: "#2ecc71", title: "Junior") { Id = 1 },
            new(color: "#27ae60", title: "Junior+") { Id = 2 },
            new(color: "#3498db", title: "Middle") { Id = 3 },
            new(color: "#2980b9", title: "Middle+") { Id = 4 },
            new(color: "#9b59b6", title: "Senior") { Id = 5 },
            new(color: "#8e44ad", title: "Senior+") { Id = 6 },
        };

        builder.HasData(data);
    }
}

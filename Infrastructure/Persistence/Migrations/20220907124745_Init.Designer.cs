// <auto-generated />
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Persistence.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20220907124745_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Domain.Entities.Level", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasMaxLength(7)
                        .HasColumnType("character varying(7)")
                        .HasColumnName("Color");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(30)
                        .HasColumnType("character varying(30)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Level", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Color = "#2ecc71",
                            Title = "Junior"
                        },
                        new
                        {
                            Id = 2,
                            Color = "#27ae60",
                            Title = "Junior+"
                        },
                        new
                        {
                            Id = 3,
                            Color = "#3498db",
                            Title = "Middle"
                        },
                        new
                        {
                            Id = 4,
                            Color = "#2980b9",
                            Title = "Middle+"
                        },
                        new
                        {
                            Id = 5,
                            Color = "#9b59b6",
                            Title = "Senior"
                        },
                        new
                        {
                            Id = 6,
                            Color = "#8e44ad",
                            Title = "Senior+"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Tag", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(150)
                        .HasColumnType("character varying(150)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Tag", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "C#"
                        },
                        new
                        {
                            Id = 2,
                            Title = "JavaScript"
                        },
                        new
                        {
                            Id = 3,
                            Title = "SQL"
                        });
                });

            modelBuilder.Entity("Domain.Entities.Topic", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Content")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("Content");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(240)
                        .HasColumnType("character varying(240)")
                        .HasColumnName("Title");

                    b.HasKey("Id");

                    b.ToTable("Topic", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TopicLevel", b =>
                {
                    b.Property<int>("TopicId")
                        .HasColumnType("integer");

                    b.Property<int>("LevelId")
                        .HasColumnType("integer");

                    b.HasKey("TopicId", "LevelId");

                    b.HasIndex("LevelId");

                    b.ToTable("TopicLevel", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TopicTag", b =>
                {
                    b.Property<int>("TopicId")
                        .HasColumnType("integer");

                    b.Property<int>("TagId")
                        .HasColumnType("integer");

                    b.HasKey("TopicId", "TagId");

                    b.HasIndex("TagId");

                    b.ToTable("TopicTag", (string)null);
                });

            modelBuilder.Entity("Domain.Entities.TopicLevel", b =>
                {
                    b.HasOne("Domain.Entities.Level", "Level")
                        .WithMany("TopicLevels")
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Topic", "Topic")
                        .WithMany("TopicLevels")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Level");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Domain.Entities.TopicTag", b =>
                {
                    b.HasOne("Domain.Entities.Tag", "Tag")
                        .WithMany("TopicTags")
                        .HasForeignKey("TagId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Entities.Topic", "Topic")
                        .WithMany("TopicTags")
                        .HasForeignKey("TopicId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tag");

                    b.Navigation("Topic");
                });

            modelBuilder.Entity("Domain.Entities.Level", b =>
                {
                    b.Navigation("TopicLevels");
                });

            modelBuilder.Entity("Domain.Entities.Tag", b =>
                {
                    b.Navigation("TopicTags");
                });

            modelBuilder.Entity("Domain.Entities.Topic", b =>
                {
                    b.Navigation("TopicLevels");

                    b.Navigation("TopicTags");
                });
#pragma warning restore 612, 618
        }
    }
}

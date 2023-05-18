using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManager.Infra.Mappings
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(80);

            builder.Property(x => x.Genre)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.UrlImage);

            builder.Property(x => x.ReleaseYear)
                .IsRequired();

            builder.Property(x => x.Developer)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(80);

            builder
                .HasIndex(x => x.Title, "IX_Game_Title")
                .IsUnique();

            builder
                .HasOne(x => x.BelongsTo)
                .WithMany(x => (IEnumerable<Game>)x.Items)
                        .HasConstraintName("FK_Game_PersonalList")
                        .OnDelete(DeleteBehavior.ClientCascade);

            builder
                .HasMany(x => x.Platforms)
                .WithMany(x => x.Games)
                .UsingEntity<Dictionary<string, object>>(
                    "GamePlatforms",
                    Game => Game
                        .HasOne<Platform>()
                        .WithMany()
                        .HasForeignKey("PlatformsId")
                        .HasConstraintName("FK_GamePlatforms_GameId")
                        .OnDelete(DeleteBehavior.Cascade),
                    Platform => Platform
                        .HasOne<Game>()
                        .WithMany()
                        .HasForeignKey("GameId")
                        .HasConstraintName("FK_GamePlatforms_PlatformId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}

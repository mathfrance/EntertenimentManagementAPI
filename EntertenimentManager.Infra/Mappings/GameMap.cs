using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Itens.Contracts;
using EntertenimentManager.Domain.Entities.Lists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManager.Infra.Mappings
{
    public class GameMap : IEntityTypeConfiguration<Game>
    {
        public void Configure(EntityTypeBuilder<Game> builder)
        {
            builder.ToTable("Game")
                .HasBaseType<Item>();

            builder.Property(x => x.Developer)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(80);

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

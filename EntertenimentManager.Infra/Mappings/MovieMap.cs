using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManager.Infra.Mappings
{
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Tabela
            builder.ToTable("Movie");

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

            builder.Property(x => x.ReleaseYear);

            builder.Property(x => x.Distributor)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(80);

            builder.Property(x => x.Director)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(80);

            builder.Property(x => x.DurationInMinutes);

            builder
                .HasIndex(x => x.Title, "IX_Movie_Title")
                .IsUnique();

            builder
                .HasOne(x => x.BelongsTo)
                .WithMany(x => (IEnumerable<Movie>)x.Items)
                        .HasConstraintName("FK_Movie_PersonalList")
                        .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

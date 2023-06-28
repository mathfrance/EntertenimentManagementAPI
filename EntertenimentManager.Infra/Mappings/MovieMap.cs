using EntertenimentManager.Domain.Entities.Itens;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManager.Infra.Mappings
{
    public class MovieMap : IEntityTypeConfiguration<Movie>
    {
        public void Configure(EntityTypeBuilder<Movie> builder)
        {
            // Tabela
            builder.ToTable("Movie")
                .HasBaseType<Item>();

            builder.Property(x => x.Distributor)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(80);

            builder.Property(x => x.Director)
               .HasColumnType("NVARCHAR")
               .HasMaxLength(80);

            builder.Property(x => x.DurationInMinutes);

        }
    }
}

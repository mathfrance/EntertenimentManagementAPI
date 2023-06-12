using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using EntertenimentManager.Domain.Entities.Itens.Contracts;
using EntertenimentManager.Domain.Entities.Itens;

namespace EntertenimentManager.Infra.Mappings
{
    public class ItemMap : IEntityTypeConfiguration<Item>
    {
        public void Configure(EntityTypeBuilder<Item> builder)
        {
            // Tabela
            builder.ToTable("Item");

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

            builder
                .HasOne(x => x.BelongsTo)
                .WithMany(x => x.Items)
                        .HasConstraintName("FK_Item_PersonalList")
                        .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

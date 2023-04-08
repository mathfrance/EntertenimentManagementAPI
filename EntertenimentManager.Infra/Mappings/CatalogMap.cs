using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManager.Infra.Mappings
{
    public class CatalogMap : IEntityTypeConfiguration<Catalog>
    {
        public void Configure(EntityTypeBuilder<Catalog> builder)
        {
            builder.ToTable("Catalog");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Description)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150);

            builder
                .HasOne(x => x.Owner)
                        .WithOne(x => x.Catalog)
                        .HasForeignKey<User>("CatalogId")
                        .HasConstraintName("FK_Catalog_User")
                        .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

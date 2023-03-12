using EntertenimentManagement.Models.Lists;
using EntertenimentManagement.Models.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManagement.API.Data.Mappings
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

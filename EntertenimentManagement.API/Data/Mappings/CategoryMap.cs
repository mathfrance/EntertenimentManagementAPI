using EntertenimentManagement.Models.Lists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Collections.Generic;

namespace EntertenimentManagement.API.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.Description)
                .HasColumnType("NVARCHAR")
                .HasMaxLength(150);

            builder
                .HasOne(x => x.Catalog)
                        .WithMany(x => x.Categories)
                        .HasConstraintName("FK_Category_Catalog")
                        .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

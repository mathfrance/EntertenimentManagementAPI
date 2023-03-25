using EntertenimentManager.Domain.Models.Lists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManager.API.Data.Mappings
{
    public class PersonalListMap : IEntityTypeConfiguration<PersonalList>
    {
        public void Configure(EntityTypeBuilder<PersonalList> builder)
        {
            builder.ToTable("PersonalList");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Title)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(50);

            builder.Property(x => x.Exclusive)
                .IsRequired()
                .HasColumnType("BIT");

            builder
                .HasOne(x => x.Category)
                        .WithMany(x => x.Lists)
                        .HasConstraintName("FK_PersonalList_Category")
                        .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

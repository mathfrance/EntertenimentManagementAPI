using EntertenimentManager.Domain.Entities.Lists;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManager.Infra.Mappings
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

            builder
                .HasOne(x => x.Category)
                        .WithMany(x => (IEnumerable<PersonalList>)x.Lists)
                        .HasConstraintName("FK_PersonalList_Category")
                        .OnDelete(DeleteBehavior.ClientCascade);
        }
    }
}

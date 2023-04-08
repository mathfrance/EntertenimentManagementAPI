using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EntertenimentManager.Infra.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(x => x.Email)
                .IsRequired()
                .HasColumnType("NVARCHAR")
                .HasMaxLength(120);

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.Image);
            //     .HasDefaultValue("Foto de perfil anonimo");

            builder
                .HasIndex(x => x.Email, "IX_User_Email")
                .IsUnique();

            builder
                .HasOne(x => x.Catalog)
                .WithOne(x => x.Owner)
                .HasForeignKey<Catalog>("UserId")
                .HasConstraintName("FK_User_Catalog")
                .OnDelete(DeleteBehavior.Cascade);

            builder
                .HasMany(x => x.Roles)
                .WithMany(x => x.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "UserRoles",
                    User => User
                        .HasOne<Role>()
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .HasConstraintName("FK_UserRoles_UserId")
                        .OnDelete(DeleteBehavior.Cascade),
                    Role => Role
                        .HasOne<User>()
                        .WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("FK_UserRoles_RoleId")
                        .OnDelete(DeleteBehavior.Cascade));
        }
    }
}

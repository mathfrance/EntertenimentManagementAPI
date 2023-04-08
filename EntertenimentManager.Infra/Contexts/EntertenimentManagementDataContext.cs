using EntertenimentManager.Domain.Entities.Itens;
using EntertenimentManager.Domain.Entities.Lists;
using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Infra.Mappings;
using Microsoft.EntityFrameworkCore;

namespace EntertenimentManager.Infra.Contexts
{
    public class EntertenimentManagementDataContext : DbContext
    {
        public EntertenimentManagementDataContext(DbContextOptions<EntertenimentManagementDataContext> options)
            : base(options)
        { }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Game> Games { get; set; }
        public DbSet<Catalog> Catalogs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<PersonalList> PersonalLists { get; set; }
        public DbSet<Platform> Platforms { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MovieMap());
            modelBuilder.ApplyConfiguration(new GameMap());
            modelBuilder.ApplyConfiguration(new CatalogMap());
            modelBuilder.ApplyConfiguration(new CategoryMap());
            modelBuilder.ApplyConfiguration(new PersonalListMap());
            modelBuilder.ApplyConfiguration(new PlatformMap());
            modelBuilder.ApplyConfiguration(new UserMap());
            modelBuilder.ApplyConfiguration(new RoleMap());
        }
    }
}
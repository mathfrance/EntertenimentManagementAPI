using EntertenimentManager.Domain.Entities.Users;
using EntertenimentManager.Infra.Contexts;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace EntertenimentManager.Tests.RepositoriesTests.Sqlite
{
    public class SqliteBuider
    {
        private readonly DbConnection _connection;
        private readonly EntertenimentManagementDataContext _context;
        private readonly DbContextOptions<EntertenimentManagementDataContext> _contextOptions;

        public SqliteBuider()
        {
            _connection = new SqliteConnection("Filename=:memory:");
            _connection.Open();

            _contextOptions = new DbContextOptionsBuilder<EntertenimentManagementDataContext>()
                .UseSqlite(_connection)
                .Options;

            _context = new EntertenimentManagementDataContext(_contextOptions);
        }

        public SqliteBuider CreateDatabase()
        {
            _context.Database.EnsureDeleted();
            _context.Database.EnsureCreated();

            return this;
        }

        public EntertenimentManagementDataContext Build()
        {
            _context.SaveChanges();

            return _context;
        }

        public SqliteBuider InicialLoad()
        {
            _context.Database.ExecuteSqlRaw("INSERT INTO [Platform](Description) VALUES ('Outros'), ('PlayStation 3'), ('PlayStation 4'), ('PlayStation 5'), ('Xbox 360'), ('Xbox One S/X'), ('Xbox Series S/X'), ('PC'), ('Nintendo Switch');");
            _context.Database.ExecuteSqlRaw("INSERT INTO[Role](Name) VALUES('admin'), ('user')");

            return this;
        }
    }
}


using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.DataContext;
using System;

namespace PhoneBook.Api.UnitTests
{
    public abstract class MemoryDbContext : IDisposable
    {
        private const string InMemoryConnectionString = "Data Source=:memory:";
        private readonly SqliteConnection _connection;

        protected readonly PhoneBookDbContext dbContext;

        protected MemoryDbContext()
        {
            _connection = new SqliteConnection(InMemoryConnectionString);
            _connection.Open();

            var options = new DbContextOptionsBuilder<PhoneBookDbContext>()
                    .UseSqlite(_connection)
                    .Options;

            dbContext = new PhoneBookDbContext(options);
            dbContext.Database.EnsureCreated();
        }

        public void Dispose()
        {
            _connection.Close();
        }
    }
}

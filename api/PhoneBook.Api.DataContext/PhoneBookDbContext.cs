using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.DomainModels;

namespace PhoneBook.Api.DataContext
{
    public class PhoneBookDbContext: DbContext
    {
        public virtual DbSet<DomainModels.PhoneBook> PhoneBooks { get; set; }

        public virtual DbSet<PhoneBookEntry> PhoneBookEntries { get; set; }

        public PhoneBookDbContext(DbContextOptions<PhoneBookDbContext> options) : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}

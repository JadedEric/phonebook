using PhoneBook.Api.DataContext;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.UnitOfWork.Interfaces;
using System;
using System.Threading.Tasks;

namespace PhoneBook.Api.UnitOfWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly PhoneBookDbContext _context;

        public IPhoneBookRepository Books { get; }

        public IPhoneBookEntryRepository Entries { get; }

        public UnitOfWork(PhoneBookDbContext context, IPhoneBookRepository booksRepository, IPhoneBookEntryRepository entriesRepository)
        {
            _context = context;
            Books = booksRepository;
            Entries = entriesRepository;
        }

        public async Task<int> CompleteAsync()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}

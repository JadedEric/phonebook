using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.DataContext;
using PhoneBook.Api.DomainModels;
using PhoneBook.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.Repositories
{
    public class PhoneBookEntryRepository : IPhoneBookEntryRepository
    {
        private readonly PhoneBookDbContext _context;

        public PhoneBookEntryRepository(PhoneBookDbContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(int id)
        {
            var entity = await _context.PhoneBookEntries.FindAsync(id);
            _context.PhoneBookEntries.Remove(entity);
            return 1;
        }

        public async Task<IEnumerable<PhoneBookEntry>> Get()
        {
            return await _context.PhoneBookEntries.ToListAsync();
        }

        public async Task<PhoneBookEntry> Get(int id)
        {
            return await _context.PhoneBookEntries.FindAsync(id);
        }

        public async Task<IReadOnlyList<PhoneBookEntry>> GetByPhoneBookId(int id)
        {
            return await _context.PhoneBookEntries.Where(entry => entry.PhoneBookId == id).ToListAsync();
        }

        public async Task<int> Post(PhoneBookEntry entity)
        {
            await _context.PhoneBookEntries.AddAsync(entity);
            return 1;
        }

        public async Task<int> Put(PhoneBookEntry entity)
        {
            var phonebookentry = await _context.PhoneBookEntries.FindAsync(entity.Id);
            _context.Entry(phonebookentry).CurrentValues.SetValues(entity);
            return 1;
        }
    }
}

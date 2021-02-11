using Microsoft.EntityFrameworkCore;
using PhoneBook.Api.DataContext;
using PhoneBook.Api.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Api.Repositories
{
    public class PhoneBookRepository : IPhoneBookRepository
    {
        private readonly PhoneBookDbContext _context;

        public PhoneBookRepository(PhoneBookDbContext context)
        {
            _context = context;
        }

        public async Task<int> Delete(int id)
        {
            var entity = await _context.PhoneBooks.FindAsync(id);
            _context.PhoneBooks.Remove(entity);
            return 1;
        }

        public async Task<IEnumerable<DomainModels.PhoneBook>> Get()
        {
            return await _context.PhoneBooks.ToListAsync();
        }

        public async Task<DomainModels.PhoneBook> Get(int id)
        {
            return await _context.PhoneBooks.FindAsync(id);
        }

        public async Task<int> Post(DomainModels.PhoneBook entity)
        {
            await _context.PhoneBooks.AddAsync(entity);
            return 1;
        }

        public async Task<int> Put(DomainModels.PhoneBook entity)
        {
            var phonebook = await _context.PhoneBooks.FindAsync(entity.Id);
            _context.Entry(phonebook).CurrentValues.SetValues(entity);
            return 1;
        }
    }
}

using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PhoneBook.Api.DomainModels;
using PhoneBook.Api.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.UnitTests
{
    public class PhoneBookEntryRepositoryTest : MemoryDbContext
    {
        private PhoneBookRepository _phoneBookRepo;
        private PhoneBookEntryRepository _phoneBookEntryRepo;

        [SetUp]
        public void Setup()
        {
            this._phoneBookRepo = new PhoneBookRepository(dbContext);
            this._phoneBookEntryRepo = new PhoneBookEntryRepository(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            // TODO: handle tear down logic here
        }

        [Test, Order(0)]
        public async Task TestTableShouldBeCreated()
        {
            var phonebookentries = await _phoneBookEntryRepo.Get();

            Assert.False(phonebookentries.Any());
        }

        [Test, Order(2)]
        public async Task TestMissingRequiredNameShouldThrowException()
        {
            var phonebookentry = new PhoneBookEntry();
            await _phoneBookEntryRepo.Post(phonebookentry);

            Assert.Throws<DbUpdateException>(() => dbContext.SaveChanges());
        }

        [Test, Order(1)]
        public async Task TestInsertedEntityShouldContainGeneratedId()
        {
            var phonebook = new DomainModels.PhoneBook
            {
                Name = "Book 1"
            };

            await _phoneBookRepo.Post(phonebook);
            await dbContext.SaveChangesAsync();

            var phonebookentry = new PhoneBookEntry
            {
                Name = "Eric",
                PhoneBookId = phonebook.Id,
                PhoneNumber = "0827074282"
            };

            await _phoneBookEntryRepo.Post(phonebookentry);
            await dbContext.SaveChangesAsync();

            Assert.NotNull(phonebookentry.Id);
        }
    }
}

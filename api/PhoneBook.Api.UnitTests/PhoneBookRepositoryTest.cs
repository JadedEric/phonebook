using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using PhoneBook.Api.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace PhoneBook.Api.UnitTests
{
    public class PhoneBookRepositoryTest : MemoryDbContext
    {
        private PhoneBookRepository _repo;

        [SetUp]
        public void Setup()
        {
            this._repo = new PhoneBookRepository(dbContext);
        }

        [TearDown]
        public void TearDown()
        {
            // TODO: handle tear down logic here
        }

        [Test, Order(0)]
        public async Task TestTableShouldBeCreated()
        {
            var phonebooks = await _repo.Get();

            Assert.False(phonebooks.Any());
        }

        [Test, Order(2)]
        public async Task TestMissingRequiredNameShouldThrowException()
        {
            var phonebook = new DomainModels.PhoneBook();
            await _repo.Post(phonebook);

            Assert.Throws<DbUpdateException>(() => dbContext.SaveChanges());
        }

        [Test, Order(1)]
        public async Task TestInsertedEntityShouldContainGeneratedId()
        {
            var phonebook = new DomainModels.PhoneBook
            {
                Name = "Book 1"
            };

            await _repo.Post(phonebook);
            await dbContext.SaveChangesAsync();

            Assert.NotNull(phonebook.Id);
        }
    }
}

using NUnit.Framework;
using System.Threading.Tasks;

namespace PhoneBook.Api.UnitTests
{
    public class PhoneBookDbContextTest : MemoryDbContext
    {
        [Test]
        public async Task TestDatabaseIsAvailable()
        {
            Assert.True(await dbContext.Database.CanConnectAsync());
        }
    }
}

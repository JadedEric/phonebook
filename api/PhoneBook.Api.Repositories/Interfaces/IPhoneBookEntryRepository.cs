using PhoneBook.Api.DomainModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Api.Repositories.Interfaces
{
    public interface IPhoneBookEntryRepository : IRepository<PhoneBookEntry>
    {
        Task<IReadOnlyList<PhoneBookEntry>> GetByPhoneBookId(int id);
    }
}

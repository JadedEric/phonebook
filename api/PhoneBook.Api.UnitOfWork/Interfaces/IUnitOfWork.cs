using PhoneBook.Api.Repositories.Interfaces;
using System;
using System.Threading.Tasks;

namespace PhoneBook.Api.UnitOfWork.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IPhoneBookRepository Books { get; }

        IPhoneBookEntryRepository Entries { get; }

        Task<int> CompleteAsync();
    }
}

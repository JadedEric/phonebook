using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Api.Repositories.Interfaces
{
    public interface IRepository<T> where T : class
    {
        Task<int> Delete(int id);

        Task<IEnumerable<T>> Get();

        Task<T> Get(int id);

        Task<int> Post(T entity);

        Task<int> Put(T entity);
    }
}

using AutoMapper;
using PhoneBook.Api.DomainModels;
using PhoneBook.Api.UnitOfWork.Interfaces;
using PhoneBook.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Api.Services
{
    public class PhoneBookEntryManager
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public PhoneBookEntryManager(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<int> Add(PhoneBookEntryViewModel entry)
        {
            var entity = _mapper.Map<PhoneBookEntry>(entry);
            await _unitofwork.Entries.Post(entity);
            return await _unitofwork.CompleteAsync();
        }

        public async Task<IReadOnlyList<PhoneBookEntryViewModel>> All()
        {
            var entries = await _unitofwork.Entries.Get();
            return _mapper.Map<IReadOnlyList<PhoneBookEntryViewModel>>(entries);
        }

        public async Task<PhoneBookEntryViewModel> ById(int id)
        {
            var entry = await _unitofwork.Entries.Get(id);
            return _mapper.Map<PhoneBookEntryViewModel>(entry);
        }

        public async Task<IReadOnlyList<PhoneBookEntryViewModel>> ByPhoneBookId(int id)
        {
            var entries = await _unitofwork.Entries.GetByPhoneBookId(id);
            return _mapper.Map<IReadOnlyList<PhoneBookEntryViewModel>>(entries);
        }

        public async Task<int> Remove(int id)
        {
            await _unitofwork.Entries.Delete(id);
            return await _unitofwork.CompleteAsync();
        }

        public async Task<int> Update(PhoneBookEntryViewModel entry)
        {
            var entity = _mapper.Map<PhoneBookEntry>(entry);
            await _unitofwork.Entries.Put(entity);
            return await _unitofwork.CompleteAsync();
        }
    }
}

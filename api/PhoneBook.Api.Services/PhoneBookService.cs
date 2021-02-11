using AutoMapper;
using PhoneBook.Api.UnitOfWork.Interfaces;
using PhoneBook.Api.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PhoneBook.Api.Services
{
    public class PhoneBookManager
    {
        private readonly IUnitOfWork _unitofwork;
        private readonly IMapper _mapper;

        public PhoneBookManager(IUnitOfWork unitofwork, IMapper mapper)
        {
            _unitofwork = unitofwork;
            _mapper = mapper;
        }

        public async Task<int> Add(PhoneBookViewModel entry)
        {
            var entity = _mapper.Map<DomainModels.PhoneBook>(entry);
            await _unitofwork.Books.Post(entity);
            return await _unitofwork.CompleteAsync();
        }

        public async Task<IReadOnlyList<PhoneBookViewModel>> All()
        {
            var entries = await _unitofwork.Books.Get();
            return _mapper.Map<IReadOnlyList<PhoneBookViewModel>>(entries);
        }

        public async Task<PhoneBookViewModel> ById(int id)
        {
            var entry = await _unitofwork.Books.Get(id);
            return _mapper.Map<PhoneBookViewModel>(entry);
        }

        public async Task<int> Remove(int id)
        {
            await _unitofwork.Books.Delete(id);
            return await _unitofwork.CompleteAsync();
        }

        public async Task<int> Update(PhoneBookViewModel entry)
        {
            var entity = _mapper.Map<DomainModels.PhoneBook>(entry);
            await _unitofwork.Books.Put(entity);
            return await _unitofwork.CompleteAsync();
        }
    }
}

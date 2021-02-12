using PhoneBook.Api.DomainModels;
using PhoneBook.Api.ViewModels;

namespace PhoneBook.Api
{
    public class AutoMapping : AutoMapper.Profile
    {
        public AutoMapping()
        {
            CreateMap<DomainModels.PhoneBook, PhoneBookViewModel>();
            CreateMap<PhoneBookViewModel, DomainModels.PhoneBook>();
            CreateMap<PhoneBookEntry, PhoneBookEntryViewModel>();
            CreateMap<PhoneBookEntryViewModel, PhoneBookEntry>();
        }
    }
}

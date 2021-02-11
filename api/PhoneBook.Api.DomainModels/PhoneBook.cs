using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PhoneBook.Api.DomainModels
{
    public class PhoneBook : EntityBase
    {
        [Required]
        public string Name { get; set; }

        public ICollection<PhoneBookEntry> Entries { get; set; } = new List<PhoneBookEntry>();
    }
}

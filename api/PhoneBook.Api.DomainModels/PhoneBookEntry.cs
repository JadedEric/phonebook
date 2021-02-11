using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhoneBook.Api.DomainModels
{
    public class PhoneBookEntry : EntityBase
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public int PhoneBookId { get; set; }

        [ForeignKey("PhoneBookId")]
        public PhoneBook PhoneBook { get; set; }
    }
}

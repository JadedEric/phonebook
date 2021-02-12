using Microsoft.AspNetCore.Mvc;
using PhoneBook.Api.Services;
using PhoneBook.Api.ViewModels;
using System.Threading.Tasks;

namespace PhoneBook.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhoneBookEntryController : ControllerBase
    {
        private readonly PhoneBookEntryService _service;

        public PhoneBookEntryController(PhoneBookEntryService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var entries = await _service.All();
            return Ok(entries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var books = await _service.ById(id);
            return Ok(books);
        }

        [HttpGet("phonebook/{id}")]
        public async Task<IActionResult> ByPhoneBookId(int id)
        {
            var books = await _service.ByPhoneBookId(id);
            return Ok(books);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PhoneBookEntryViewModel phoneBookModel)
        {
            var result = await _service.Add(phoneBookModel);
            return Ok(result);
        }

        [HttpPut]
        public async Task<IActionResult> Put([FromBody] PhoneBookEntryViewModel phoneBookModel)
        {
            var result = await _service.Update(phoneBookModel);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _service.Remove(id);
            return Ok(result);
        }
    }
}

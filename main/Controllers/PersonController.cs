using main.Models;
using main.Services;
using Microsoft.AspNetCore.Mvc;

namespace main.Controllers
{
    [ApiController]
    [Route("v1/api/persons")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        public PersonController(IPersonService personService) { 
            _personService = personService;
        }

        [HttpGet]
        public IActionResult GetAllPersons() {
            return Ok(_personService.findAll());
        }

        [HttpGet("{id}")]
        public IActionResult FindPersonById(long id)
        {
            return Ok(_personService.findById(id));
        }

        [HttpPost("new")]
        public IActionResult createPerson(Person person)
        {
            return Ok(_personService.createPerson(person));
        }

        [HttpPut("update")]
        public IActionResult UpdatePerson(Person person)
        {
            return Ok(_personService.updatePerson(person));
        }
        
    }
}

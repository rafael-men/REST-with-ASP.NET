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
    }
}

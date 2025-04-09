using main.Repository;
using main.Models;
using Microsoft.AspNetCore.Mvc;
using main.Business;
using main.VO;

namespace main.Controllers
{
    [ApiController]
    [Route("v1/api/persons")]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness) { 
            _personBusiness = personBusiness;
        }

        [HttpGet]
        public IActionResult GetAllPersons() {
            return Ok(_personBusiness.findAll());
        }

        [HttpGet("{id}")]
        public IActionResult FindPersonById(long id)
        {
            return Ok(_personBusiness.findById(id));
        }

        [HttpPost("new")]
        public IActionResult createPerson(PersonVO person)
        {
            return Ok(_personBusiness.createPerson(person));
        }

        [HttpPut("update")]
        public IActionResult UpdatePerson(PersonVO person)
        {
            return Ok(_personBusiness.updatePerson(person));
        }
        
    }
}

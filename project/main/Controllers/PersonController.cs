using main.Repository;
using main.Models;
using Microsoft.AspNetCore.Mvc;
using main.Business;
using main.VO;
using main.Hypermedia.Filters;
using Microsoft.AspNetCore.Authorization;

namespace main.Controllers
{
    [ApiController]
    [Route("v1/api/persons")]
    [Authorize]
    public class PersonController : ControllerBase
    {
        private readonly IPersonBusiness _personBusiness;

        public PersonController(IPersonBusiness personBusiness) { 
            _personBusiness = personBusiness;
        }

        [HttpGet]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult GetAllPersons() {
            return Ok(_personBusiness.findAll());
        }

        [HttpGet("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult FindPersonById(long id)
        {
            return Ok(_personBusiness.findById(id));
        }

        [HttpPost("new")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult createPerson(PersonVO person)
        {
            return Ok(_personBusiness.createPerson(person));
        }

        [HttpPut("update")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult UpdatePerson([FromBody]PersonVO person)
        {
            return Ok(_personBusiness.updatePerson(person));
        }

        [HttpDelete("{id}")]
        [TypeFilter(typeof(HyperMediaFilter))]
        public IActionResult DeletePerson(long id)
        {
            _personBusiness.deletePerson(id);
            return NoContent();
        }
        
    }
}

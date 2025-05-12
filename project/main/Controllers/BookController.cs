using main.Business;
using main.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace main.Controllers
{

    [ApiController]
    [Route("v1/api/books")]
    [Authorize]
    public class BookController : ControllerBase
    {
        private readonly IBookBusiness _bookBusiness;

        public BookController(IBookBusiness bookBusiness)
        {
            _bookBusiness = bookBusiness;
        }

        [HttpGet]
        public IActionResult GetAllBooks()
        {
            return Ok(_bookBusiness.findAll());
        }

        [HttpGet("{id}")]
        public IActionResult FindBookById(long id)
        {
            return Ok(_bookBusiness.findById(id));
        }

        [HttpPost("new")]
        public IActionResult createBook(Book book)
        {
            return Ok(_bookBusiness.createBook(book));
        }

        [HttpPut("update")]
        public IActionResult UpdatePerson(Book book)
        {
            return Ok(_bookBusiness.updateBook(book));
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteBook(long id)
        {
            _bookBusiness.deleteBook(id);
            return NoContent();
        }
    }
}

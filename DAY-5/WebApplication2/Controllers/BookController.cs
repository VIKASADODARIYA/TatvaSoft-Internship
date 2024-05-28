using DataLayer.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;
using ServiceLayer.Model;

namespace WebApplication2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookService _bookService;

        public BookController(BookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public IActionResult GetAllBook()
        {
            var result = _bookService.GetAll();
            return Ok(result);
        }

        [Authorize]
        [HttpGet("GetBookById")]
        public IActionResult GetBookById(int id)
        {
            var result = _bookService.GetById(id);
            return Ok(result);
        }

        [Authorize(Roles ="Admin")]
        [HttpPost]
        public async Task<IActionResult> InsertBook(BookDetails book)
        {
            await _bookService.InsertBookDetails(book);
            return Ok();
        }
    }
}

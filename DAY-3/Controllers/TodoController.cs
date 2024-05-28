// Controllers/TodoController.cs
using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;

namespace TodoApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoController : ControllerBase
    {
        private readonly TodoService _todoService;

        public TodoController(TodoService todoService)
        {
            _todoService = todoService;
        }

        [HttpGet]
        public ActionResult<IEnumerable<TodoItem>> GetTodoItems()
        {
            return Ok(_todoService.GetAll());
        }

        [HttpGet("{id}")]
        public ActionResult<TodoItem> GetTodoItem(long id)
        {
            var item = _todoService.GetById(id);
            if (item == null)
            {
                return NotFound();
            }
            return Ok(item);
        }

        [HttpPost]
        public ActionResult<TodoItem> PostTodoItem(TodoItem item)
        {
            _todoService.Add(item);
            return CreatedAtAction(nameof(GetTodoItem), new { id = item.Id }, item);
        }

        [HttpPut("{id}")]
        public IActionResult PutTodoItem(long id, TodoItem item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _todoService.Update(item);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteTodoItem(long id)
        {
            var existingItem = _todoService.GetById(id);
            if (existingItem == null)
            {
                return NotFound();
            }

            _todoService.Delete(id);
            return NoContent();
        }
    }
}

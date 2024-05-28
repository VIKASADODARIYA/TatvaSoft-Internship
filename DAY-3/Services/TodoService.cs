// Services/TodoService.cs
using TodoApp.Models;
using System.Collections.Generic;
using System.Linq;

namespace TodoApp.Services
{
    public class TodoService
    {
        private readonly List<TodoItem> _items;

        public TodoService()
        {
            // Initialize with some default items
            _items = new List<TodoItem>
            {
                new TodoItem { Id = 1, Name = "Learn ASP.NET Core", IsComplete = false },
                new TodoItem { Id = 2, Name = "Build a Todo app", IsComplete = false },
                new TodoItem { Id = 3, Name = "Deploy the app", IsComplete = false }
            };
        }

        public IEnumerable<TodoItem> GetAll() => _items;

        public TodoItem GetById(long id) => _items.FirstOrDefault(x => x.Id == id);

        public void Add(TodoItem item)
        {
            item.Id = _items.Count > 0 ? _items.Max(x => x.Id) + 1 : 1;
            _items.Add(item);
        }

        public void Update(TodoItem item)
        {
            var index = _items.FindIndex(x => x.Id == item.Id);
            if (index != -1)
            {
                _items[index] = item;
            }
        }

        public void Delete(long id)
        {
            var item = _items.FirstOrDefault(x => x.Id == id);
            if (item != null)
            {
                _items.Remove(item);
            }
        }
    }
}

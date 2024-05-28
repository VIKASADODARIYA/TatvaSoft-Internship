using System.Collections.Generic;
using System.Linq;
using Test.Data;
using Test.Models;

namespace Test.Service
{
    public class BookService
    {
        private readonly BookDbContext _context;

        public BookService(BookDbContext context)
        {
            _context = context;
        }

        // Get all books
        public List<Book> GetAll() => _context.Books.ToList();

        // Get book by ID
        public Book GetById(int id) => _context.Books.FirstOrDefault(b => b.Id == id);

        // Add a new book
        public void Add(Book book)
        {
            _context.Books.Add(book);
            _context.SaveChanges();
        }

        // Update an existing book
        public void Update(Book book)
        {
            var existingBook = _context.Books.Find(book.Id);
            if (existingBook != null)
            {
                existingBook.Title = book.Title;
                existingBook.Author = book.Author;
                existingBook.Genre = book.Genre;
                // Update other properties as needed

                _context.SaveChanges(); // Save changes after updating
            }
        }

        // Delete a book by ID
        public void Delete(int id)
        {
            var book = _context.Books.FirstOrDefault(b => b.Id == id);
            if (book != null)
            {
                _context.Books.Remove(book);
                _context.SaveChanges(); // Save changes after deleting
            }
        }
    }
}

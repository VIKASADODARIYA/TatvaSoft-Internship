using DataLayer;
using DataLayer.Entities;
using ServiceLayer.Model;

namespace ServiceLayer
{
    public class BookService
    {
        private readonly List<Book> _book;
        private readonly BookRepository _bookRepository;

        public BookService(BookRepository bookRepository)
        {
            _book = new List<Book>() {
              new Book() { Id = 1 , Title = "Book1" , Author = "Kishan" , Genre ="First"},
              new Book() { Id = 2 , Title = "Book2" , Author = "Kishan" , Genre ="First"}
            };
            _bookRepository = bookRepository;
        }

        public List<Book> GetAll()
        {
            return _book;
        }

        public Book GetById(int id)
        {
            return _book.First(x => x.Id == id);
        }

        public List<Book> InsertBook(Book book)
        {
            _book.Add(book);
            return _book;
        }

        public async Task InsertBookDetails(BookDetails bookDetails)
        {
            await _bookRepository.InsertBook(bookDetails);
        }
    }
}

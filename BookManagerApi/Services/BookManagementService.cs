using BookManagerApi.Models;

namespace BookManagerApi.Services
{
	public class BookManagementService : IBookManagementService
	{
        private readonly BookContext _context;

        public BookManagementService(BookContext context)
        {
            _context = context;
        }


        public List<Book> GetAllBooks()
        {
            var books = _context.Books.ToList();
            return books;
        }

        public Book Create(Book book)
        {
            if (!BookExists(book.Id))
            {
                _context.Add(book);
                _context.SaveChanges();
                return book;
            }
            else throw new Exception($"The book with id = {book.Id} already is in our list");
        }

        public Book Update(long id, Book book)
        {
            var existingBookFound = FindBookById(id);

            existingBookFound.Title = book.Title;
            existingBookFound.Description = book.Description;
            existingBookFound.Author = book.Author;
            existingBookFound.Genre = book.Genre;

            _context.SaveChanges();
            return book;
        }

        public Book FindBookById(long id)
        {
            if (BookExists(id))
            {
                var book = _context.Books.Find(id);
                return book;
            }
            else throw new Exception($"There is no book with id = {id} in our list");
        }

        public bool BookExists(long id)
        {
            return _context.Books.Any(b => b.Id == id);
        }
        public Book DeleteById (long id)
        {
            var bookToDelete = FindBookById(id);
            _context.Remove(bookToDelete);
            _context.SaveChanges();
            return bookToDelete;
        }
    }
}


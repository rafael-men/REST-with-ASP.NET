using System;
using main.Data;
using main.Models;

namespace main.Repository.Implementations
{
    public class BooksRepositoryImpl : IBooksRepository
    {
        private PostgreSqlContext _context;

        public BooksRepositoryImpl(PostgreSqlContext context)
        {
            _context = context;
        }

        public List<Book> FindAll()
        {
            return _context.Books.ToList();
        }

        public Book FindById(long id)
        {
            return _context.Books.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Book CreateBook(Book book)
        {
            try
            {
                _context.Books.Add(book);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return book;
        }

        public Book UpdateBook(Book book)
        {
            if (!Exists(book.Id))
            {
                return new Book();
            }

            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(book.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(book);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return book;
        }

        public void DeleteBook(long id)
        {
            var result = _context.Books.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Books.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Exists(long id)
        {
            return _context.Books.Any(p => p.Id.Equals(id));
        }
    }
}

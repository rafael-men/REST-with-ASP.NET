using main.Models;
using main.Repository;

namespace main.Business.Implementations
{
    public class BookBusinessImpl : IBookBusiness
    {
        private readonly IBooksRepository _repository;

        public BookBusinessImpl(IBooksRepository booksRepository)
        {
            _repository = booksRepository;
        }

        public Book createBook(Book book)
        {
            return _repository.CreateBook(book);
        }

        public void deleteBook(long id)
        {
            _repository.DeleteBook(id);
        }

        public List<Book> findAll()
        {
            return _repository.FindAll();
        }

        public Book findById(long id)
        {
            return _repository.FindById(id);
        }

        public Book updateBook(Book book)
        {
            return _repository.UpdateBook(book);
        }
    }
}

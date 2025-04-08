using main.Models;
using main.Repository;

namespace main.Business.Implementations
{
    public class BookBusinessImpl : IBookBusiness
    {
        private readonly IGenericRepository<Book> _repository;

        public BookBusinessImpl(IGenericRepository<Book> booksRepository)
        {
            _repository = booksRepository;
        }

        public Book createBook(Book book)
        {
            return _repository.Create(book);
        }

        public void deleteBook(long id)
        {
            _repository.Delete(id);
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
            return _repository.Update(book);
        }
    }
}

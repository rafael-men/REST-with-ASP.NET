using main.Models;

namespace main.Business
{
    public interface IBookBusiness
    {
        Book createBook(Book book);
        Book findById(long id);
        List<Book> findAll();
        Book updateBook(Book book);
        void deleteBook(long id);
    }
}


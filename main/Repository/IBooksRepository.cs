using main.Models;

namespace main.Repository
{
    public interface IBooksRepository
    {
        Book CreateBook(Book book);

        Book FindById(long id);

        List<Book> FindAll();

        Book UpdateBook(Book book);

        void DeleteBook(long id);

        bool Exists(long id);
    }
}

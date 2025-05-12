using main.Models;

namespace main.Repository
{
    public interface IPersonRepository
    {
        Person createPerson(Person person);
        Person findById(long id);
        List<Person> findAll();
        Person updatePerson(Person person);
        void deletePerson(long id);

        bool Exists (long id);
    }
}

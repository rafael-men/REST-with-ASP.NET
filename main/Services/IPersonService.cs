using main.Models;

namespace main.Services
{
    public interface IPersonService
    {
        Person createPerson(Person person);
        Person findById(long id);
        List<Person> findAll();
        Person updatePerson(Person person);
        void deletePerson(long id);
    }
}

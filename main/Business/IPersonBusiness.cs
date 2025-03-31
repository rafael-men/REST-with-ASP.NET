using main.Models;

namespace main.Business
{
    public interface IPersonBusiness
    {
        Person createPerson(Person person);
        Person findById(long id);
        List<Person> findAll();
        Person updatePerson(Person person);
        void deletePerson(long id);
    }
}

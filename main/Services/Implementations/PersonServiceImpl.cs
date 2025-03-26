using main.Models;

namespace main.Services.Implementations
{
    public class PersonServiceImpl : IPersonService
    {
        public Person createPerson(Person person)
        {
            return person;
        }

        public void deletePerson(long id)
        {
            throw new NotImplementedException();
        }

        public List<Person> findAll()
        {
            throw new NotImplementedException();
        }

        public Person findById(long id)
        {
            throw new NotImplementedException();
        }

        public Person updatePerson(Person person)
        {
            throw new NotImplementedException();
        }
    }
}

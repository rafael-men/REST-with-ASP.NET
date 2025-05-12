using main.Models;
using main.VO.Converter.Contract;

namespace main.VO.Converter
{
    public class PersonConverter : IParser<PersonVO, Person>, IParser<Person, PersonVO>
    {
        public PersonVO parse(Person origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return new PersonVO
                {
                    Id = origin.Id,
                    FirstName = origin.FirstName,
                    LastName = origin.LastName,
                    Address = origin.Address,
                    Gender = origin.Gender,
                };
            }
        }

        public List<PersonVO> parse(List<Person> origin)
        {
            if (origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => parse(item)).ToList();
            }
        }

        public Person parse(PersonVO origin)
        {
            if(origin == null)
            {
                return null;
            }
            else
            {
                return new Person
                {
                    Id =origin.Id,
                    FirstName = origin.FirstName,
                    LastName = origin.LastName,
                    Address = origin.Address,   
                    Gender = origin.Gender,
                };
            }
        }

        public List<Person> parse(List<PersonVO> origin)
        {
            if(origin == null)
            {
                return null;
            }
            else
            {
                return origin.Select(item => parse(item)).ToList();
            }
        }
    }
}

using main.Models;
using main.VO;

namespace main.Business
{
    public interface IPersonBusiness
    {
        PersonVO createPerson(PersonVO person);
        PersonVO findById(long id);
        List<PersonVO> findAll();
        PersonVO updatePerson(PersonVO person);
        void deletePerson(long id);
    }
}

using System;
using main.Data;
using main.Models;
using main.Repository;
using main.VO;
using main.VO.Converter;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace main.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        private readonly PersonConverter _converter;

        public PersonBusinessImpl(IPersonRepository personRepository)
        {
            _repository=personRepository;
            _converter=new PersonConverter();
        }

        public PersonVO createPerson(PersonVO person)
        {
            var validGenders = new[] { "Masculino", "Feminino", "Não-Binário" };

            if (!validGenders.Contains(person.Gender))
            {
                throw new BadHttpRequestException("Gênero Inválido");
            }


            var personEntity = _converter.parse(person);
            personEntity = _repository.createPerson(personEntity);
            return _converter.parse(personEntity);
        }

        public void deletePerson(long id)
        {
            _repository.deletePerson(id);
        }

        public List<PersonVO> findAll()
        {
            return _converter.parse(_repository.findAll());
        }

        public PersonVO findById(long id)
        {
            return _converter.parse(_repository.findById(id));
        }

        public PersonVO updatePerson(PersonVO person)
        {
            var personEntity = _converter.parse(person);
            personEntity = _repository.updatePerson(personEntity);
            return _converter.parse(personEntity);
        }
    }
}

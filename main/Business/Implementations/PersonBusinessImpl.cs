using System;
using main.Data;
using main.Models;
using main.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace main.Business.Implementations
{
    public class PersonBusinessImpl : IPersonBusiness
    {
        private readonly IPersonRepository _repository;

        public PersonBusinessImpl(IPersonRepository personRepository)
        {
            _repository=personRepository;
        }

        public Person createPerson(Person person)
        {
            return _repository.createPerson(person);
        }

        public void deletePerson(long id)
        {
            _repository.deletePerson(id);
        }

        public List<Person> findAll()
        {
            return _repository.findAll();
        }

        public Person findById(long id)
        {
            return _repository.findById(id);
        }

        public Person updatePerson(Person person)
        {
            return _repository.updatePerson(person);
        }
    }
}

using System;
using main.Data;
using main.Models;
using main.Repository;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace main.Repository.Implementations
{
    public class PersonRepositoryImpl : IPersonRepository
    {
        private PostgreSqlContext _context;

        public PersonRepositoryImpl(PostgreSqlContext context)
        {
            _context = context;
        }

        public Person createPerson(Person person)
        {
            try
            {
                _context.Persons.Add(person);
                _context.SaveChanges();
            }
            catch (Exception ex) {
                throw ex;
            }
            return person;
        }

        public void deletePerson(long id)
        {
            var result = _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _context.Persons.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }

        }

        public List<Person> findAll()
        {
            return _context.Persons.ToList();
        }

        public Person findById(long id)
        {
            return _context.Persons.SingleOrDefault(p => p.Id.Equals(id));
        }

        public Person updatePerson(Person person)
        {
            if(!Exists(person.Id))
            {
                return new Person();
            }

            var result = _context.Persons.SingleOrDefault(p=>p.Id.Equals(person.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(person);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
            return person;
        }

        public bool Exists(long id)
        {
            return _context.Persons.Any(p => p.Id.Equals(id));
        }
    }
}

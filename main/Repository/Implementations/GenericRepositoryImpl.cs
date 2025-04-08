using System.Data;
using main.Data;
using main.Models.Base;
using Microsoft.EntityFrameworkCore;

namespace main.Repository.Implementations
{
    public class GenericRepositoryImpl<T> : IGenericRepository<T> where T : BaseEntity
    {

        private PostgreSqlContext _context;

        private DbSet<T> _dbSet;



        public GenericRepositoryImpl(PostgreSqlContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }


        public T Create(T item)
        {
            try
            {
                _dbSet.Add(item);
                _context.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            return item;
        }

        public void Delete(long id)
        {
            var result = _dbSet.SingleOrDefault(p => p.Id.Equals(id));
            if (result != null)
            {
                try
                {
                    _dbSet.Remove(result);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        public bool Exists(long id)
        {
            return _dbSet.Any(p => p.Id.Equals(id));
        }

        public List<T> FindAll()
        {
            return _dbSet.ToList();
        }

        public T FindById(long id)
        {
            return _dbSet.SingleOrDefault(p => p.Id.Equals(id));
        }

        public T Update(T item)
        {
            var result = _dbSet.SingleOrDefault(p => p.Id.Equals(item.Id));
            if (result != null)
            {
                try
                {
                    _context.Entry(result).CurrentValues.SetValues(item);
                    _context.SaveChanges();
                    return result;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            } else
            {
                return null;
            }
        }
    }
}

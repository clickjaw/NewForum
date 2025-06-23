using Microsoft.EntityFrameworkCore;

namespace ROHForum.Data
{
    public interface IGenericRepository<T> where T : class
    {
        T GetByID(params object[] id);
        void Create(T entity);
        void Delete(T entity);
        void Update(T entity);
        List<T> GetAll();
    }
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected DatabaseContext _context;
        internal DbSet<T> _dbSet;

        public GenericRepository(DatabaseContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public virtual T GetByID(params object[] id)
        {
            return _dbSet.Find(id);
        }

        public virtual void Create(T entity)
        {
            _dbSet.Add(entity);
        }

        public virtual List<T> GetAll()
        {
            return _dbSet.ToList();
        }

        public virtual void Update(T entity)
        {

            throw new NotImplementedException();
        }



        public virtual void Delete(T entity)
        {
            throw new NotImplementedException();
        }


    }
}

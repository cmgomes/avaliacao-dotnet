using Neppo.Contexts;
using Neppo.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Neppo.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected Context _context;
        readonly DbSet<T> _entidades;

        public Repository(Context contexto){
            _context = contexto;
            _entidades = contexto.Set<T>();
            Save();
        }

        public void Add(T entidade)
        {
            _context.Set<T>().Add(entidade);
            Save();
        }

        public void Update(T entidade)
        {
            _context.Set<T>().Update(entidade);
            Save();
        }

        public void Delete(T entidade)
        {
            _context.Set<T>().Remove(entidade);
            Save();
        }

        public T Get<TKey>(TKey id)
        {
            return _entidades.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _entidades;
        }

        private void Save() {
            _context.SaveChanges();
        }
    }
}

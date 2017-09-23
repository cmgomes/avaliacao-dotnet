using System.Linq;

namespace Neppo.Repositories.Interfaces
{
    public interface IRepository<T> where T : class 
    {
         T Get<TKey>(TKey id);
         IQueryable<T> GetAll();
         void Add(T entidade);
         void Update(T entidade);
         void Delete(T entidade);
    }
}

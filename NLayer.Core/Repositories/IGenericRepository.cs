using System.Linq.Expressions;

namespace NLayer.Core.Repositories
{
    public interface IGenericRepository<T> where T : class 
    {
        Task<T> GetByIdAsync(int id);
        IQueryable<T> GetAll();

        // productRepository.where(x=>x.id>5).ToListAsync(); ToList çağrıldığında veritabanına sorgu gider. Oraya kadar olan işlemler IQueryabledir.
        // ToList demeden veritabanına gitmez, List olarak tanımlarsak direkt gider, IQueryable Dolayısıyla daha performanslı.
        IQueryable<T> Where(Expression<Func<T, bool>> expression);
        Task<bool> AnyAsync(Expression<Func<T, bool>> expression);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Update(T entity); //Update'in async metodu yok.
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
    }
}

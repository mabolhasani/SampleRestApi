using System;
using System.Linq;
using System.Threading.Tasks;

namespace SampleRestApi.DataAccess.Repository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T GetById(Guid id);

        Task<T> GetByIdAsync(Guid id);

        void Create(T entity);

        void Update(T entity);

        void Delete(Guid id);
    }
}
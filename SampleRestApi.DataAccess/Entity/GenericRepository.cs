using SampleRestApi.DataAccess.EntityInterface;
using SampleRestApi.DataAccess.Repository;

namespace SampleRestApi.DataAccess.Entity
{
    internal class GenericRepository<T> : Repository<T>, IGenericRepository<T> where T : class
    {
        public GenericRepository(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
        }
    }
}

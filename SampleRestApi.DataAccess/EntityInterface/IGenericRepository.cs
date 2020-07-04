using SampleRestApi.DataAccess.Repository;

namespace SampleRestApi.DataAccess.EntityInterface
{
    public interface IGenericRepository<T> : IRepository<T> where T : class
    {
    }
}

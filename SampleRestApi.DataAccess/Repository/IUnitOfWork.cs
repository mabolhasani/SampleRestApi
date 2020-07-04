using NHibernate;
using System.Threading.Tasks;

namespace SampleRestApi.DataAccess.Repository
{
    public interface IUnitOfWork
    {
        ISession Session { get; set; }

        void BeginTransaction();

        void Commit();

        System.Threading.Tasks.Task CommitAsync();

        void Rollback();
    }
}
using System;
using System.Linq;
using System.Threading.Tasks;
using NHibernate;

namespace SampleRestApi.DataAccess.Repository
{
    public abstract class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;

        protected Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        protected ISession Session => _unitOfWork.Session;

        public virtual IQueryable<T> GetAll()
        {
            return Session.Query<T>();
        }

        public virtual T GetById(Guid id)
        {
            return Session.Get<T>(id);
        }

        public Task<T> GetByIdAsync(Guid id)
        {
            return Session.GetAsync<T>(id);
        }

        public virtual void Create(T entity)
        {
            Session.Save(entity);
        }

        public virtual void Update(T entity)
        {
            Session.Update(entity);
        }

        public void Delete(Guid id)
        {
            Session.Delete(Session.Load<T>(id));
        }
    }
}

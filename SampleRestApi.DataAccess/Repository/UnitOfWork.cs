using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Tool.hbm2ddl;
using SampleRestApi.DataAccess.AppConfig;
using System.Data;

using System.Reflection;
using System.Threading.Tasks;

namespace SampleRestApi.DataAccess.Repository
{
    internal class UnitOfWork : IUnitOfWork
    {
        private static readonly ISessionFactory SessionFactory;

        private ITransaction _transaction;

        private ISession session;
        public ISession Session
        {
            get { return session; }
            set { session = SessionFactory.OpenSession(); }
        }

        static UnitOfWork()
        {
            // Initialise singleton instance of ISessionFactory, static constructors are only executed once during the 
            // application lifetime - the first time the UnitOfWork class is used
            SessionFactory = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2012.ConnectionString(AppConfiguration.GetConnectionString()))
                .Mappings(m => m.FluentMappings
                    .AddFromAssembly(Assembly.GetExecutingAssembly()))
                .ExposeConfiguration(config => new SchemaUpdate(config).Execute(false, true))
                .BuildSessionFactory();
        }

        public UnitOfWork()
        {
            Session = SessionFactory.OpenSession();
        }

        public void BeginTransaction()
        {
            _transaction = Session.BeginTransaction(IsolationLevel.ReadCommitted);
        }

        public void Commit()
        {
            try
            {
                // commit transaction if there is one active
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Commit();
            }
            catch
            {
                // rollback if there was an exception
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();

                throw;
            }
            finally
            {
                _transaction.Dispose();
                Session.Dispose();
                Session = null;
                _transaction = null;
            }
        }

        public void Rollback()
        {
            try
            {
                if (_transaction != null && _transaction.IsActive)
                    _transaction.Rollback();
            }
            finally
            {
                _transaction.Dispose();
                Session.Dispose();
                Session = null;
                _transaction = null;
            }
        }

        public async Task CommitAsync()
        {
            try
            {
                // commit transaction if there is one active
                if (_transaction != null && _transaction.IsActive)
                    await _transaction.CommitAsync();
            }
            catch
            {
                // rollback if there was an exception
                if (_transaction != null && _transaction.IsActive)
                    await _transaction.RollbackAsync();

                throw;
            }
            finally
            {
                _transaction.Dispose();
                Session.Dispose();
                Session = null;
                _transaction = null;
            }
        }
    }
}

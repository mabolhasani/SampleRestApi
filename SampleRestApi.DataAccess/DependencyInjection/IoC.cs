
using Microsoft.Extensions.DependencyInjection;
using SampleRestApi.DataAccess.Entity;
using SampleRestApi.DataAccess.EntityInterface;
using SampleRestApi.DataAccess.Repository;

namespace SampleRestApi.DataAccess.DependencyInjection
{
    public static class IoC
    {
        public static void AddDataAccessHandlers(this IServiceCollection services)
        {
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        }
    }
}

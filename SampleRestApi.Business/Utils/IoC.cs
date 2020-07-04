using MediatR;
using Microsoft.Extensions.DependencyInjection;
using SampleRestApi.DataAccess.DependencyInjection;
using System.Reflection;

namespace SampleRestApi.Business.Utils
{
    public static class IoC
    {
        public static void AddBusinessHandlers(this IServiceCollection services)
        {
            services.AddDataAccessHandlers();

            services.AddMediatR(typeof(IoC).GetTypeInfo().Assembly);
        }
    }
}

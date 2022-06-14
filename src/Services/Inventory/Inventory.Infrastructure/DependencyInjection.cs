using Inventory.Application.Contracts.Persistence;
using Microsoft.Extensions.DependencyInjection;

namespace Inventory.Infrastructure
{
    /// <summary>
    /// Class that centralizes dependency injection management for the Infrastrucutre layer. Single Responsability
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Extension method to register all injection dependencies
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            //services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>));
            //services.AddScoped<IProductRepository, ProductRepository>();

            return services;
        }
    }
}

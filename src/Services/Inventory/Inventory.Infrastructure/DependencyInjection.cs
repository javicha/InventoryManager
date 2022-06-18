using Coravel;
using Inventory.Application.Contracts.Infrastructure;
using Inventory.Application.Contracts.Persistence;
using Inventory.Infrastructure.Mail;
using Inventory.Infrastructure.Persistence;
using Inventory.Infrastructure.Repositories;
using Inventory.Infrastructure.ScheduledJobs;
using Microsoft.EntityFrameworkCore;
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
            services.AddDbContext<InventoryContext>(options =>
                options.UseInMemoryDatabase(databaseName: "GoalSystems_Inventory"));

            services.AddScoped(typeof(IAsyncRepository<>), typeof(RepositoryBase<>)); //Per-request lifecycle
            services.AddScoped<IProductRepository, ProductRepository>(); //Per-request lifecycle

            services.AddTransient<IEmailService, EmailService>(); //each time the service is requested, a new instance is created

            //Scheduler
            services.AddScheduler();
            services.AddTransient<ProductsExpiredJob>();

            return services;
        }
    }
}

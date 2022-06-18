using FluentValidation;
using Inventory.Application.Behaviours;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Inventory.Application
{
    /// <summary>
    /// Class that centralizes dependency injection management for the Application layer. Single Responsability
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Extension method to register all injection dependencies
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddMediatR(Assembly.GetExecutingAssembly());

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

            return services;
        }
    }
}

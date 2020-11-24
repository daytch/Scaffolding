namespace Scaffolding.API.Extensions
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Scaffolding.API.Factories;
    using Scaffolding.ApplicationServices.Commands.PostEmployee;
    using Scaffolding.DataAccess;
    using Scaffolding.DataAccess.Repositories;
    using Scaffolding.Domain.Repositories;
    using Scaffolding.Domain.SeedWork;
    using System.Reflection;
    using FluentValidation;
    using MediatR;
    using Scaffolding.ApplicationServices.Queries.GetEmployee;

    public static class ServiceCollectionDependenciesExtensions
    {
        public static IServiceCollection ConfigureDependencies(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            serviceCollection.AddScoped<IUnitOfWork, UnitOfWork>();
            serviceCollection.AddScoped<ICommandFactory, CommandFactory>();

            serviceCollection.AddScoped<IEmployeeRepository, EmployeeRepository>();
            serviceCollection.AddMediatR(typeof(PostEmployeeCommandHandler).GetTypeInfo().Assembly);
            serviceCollection.AddMediatR(typeof(GetEmployeeQueryHandler).GetTypeInfo().Assembly);

            return serviceCollection;
        }
    }
}

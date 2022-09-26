using Application.Interfaces;
using Application.UseCase;
using Microsoft.Extensions.DependencyInjection;
using Model;
using Model.Interfaces;
using Repository;
using Repository.Interfaces;

namespace Api.Configuration
{
    public static class DependencyInjectionConfig
    {
        public static IServiceCollection ResolveDependencies(this IServiceCollection services)
        {
            services.AddScoped<INotifier, Notifier>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();

            services.AddScoped<ICompanyUseCase, CompanyUseCase>();

            return services;
        }
    }
}

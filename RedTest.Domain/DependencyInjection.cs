using Microsoft.Extensions.DependencyInjection;
using RedTest.Domain.Services;
using RedTest.Shared.Services;

namespace RedTest.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainDependencies(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ITopUpService, TopUpService>();
            services.AddTransient<IBeneficiaryService, BeneficiaryService>();
            return services;
        }
    }
}

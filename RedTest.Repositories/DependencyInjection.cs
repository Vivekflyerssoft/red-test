using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedTest.Repositories.Repositories;
using RedTest.Shared.Repositories;

namespace RedTest.Repositories
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            services.AddTransient<IBeneficiaryRepository, BeneficiaryRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<ITopUpRepository, TopUpRepository>();
            return services;
        }
    }
}

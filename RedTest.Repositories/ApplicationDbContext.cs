using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RedTest.Shared.Entities;
using System.Configuration;
using System.Reflection.Metadata;

namespace RedTest.Repositories
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Beneficiary> Beneficiaries { get; set; }
        public DbSet<TopUp> TopsUps { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TopUp>()
                .Property(b => b.DateCreated)
                .HasDefaultValueSql("getdate()");

            modelBuilder.Entity<User>()
                .HasMany(e => e.Beneficiaries)
                .WithOne(e => e.User)
                .HasForeignKey(e => e.UserId)
                .HasPrincipalKey(e => e.Id);

            modelBuilder.Entity<Beneficiary>()
                .HasMany(e => e.TopUps)
                .WithOne(e => e.Beneficiary)
                .HasForeignKey(e => e.BeneficiaryId)
                .HasPrincipalKey(e => e.Id);
        }
    }

    public static class DependencyInjection
    {
        public static IServiceCollection AddRepositoryDependencies(this IServiceCollection services, string connectionString)
        {
            services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(connectionString));
            return services;
        }
    }
}

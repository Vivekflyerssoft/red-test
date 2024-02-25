namespace RedTest.Repositories.Repositories
{
    public abstract class RepositoryBase
    {
        protected ApplicationDbContext _dbContext;
        protected RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }
    }
}
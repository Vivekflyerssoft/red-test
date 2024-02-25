namespace RedTest.Repositories
{
    public abstract class RepositoryBase
    {
        protected ApplicationDbContext _dbContext;
        protected RepositoryBase(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
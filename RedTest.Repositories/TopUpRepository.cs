using Microsoft.EntityFrameworkCore;
using RedTest.Shared.Entities;
using RedTest.Shared.Repositories;

namespace RedTest.Repositories
{
    public class TopUpRepository : RepositoryBase, ITopUpsRepository
    {
        private readonly DbSet<TopUp> _topUps;

        public TopUpRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _topUps = dbContext.TopsUps;
        }

        public async Task<bool> Add(TopUp topUp)
        {
            _topUps.Add(topUp);
            var result = await _dbContext.SaveChangesAsync();
            return result == 0;
        }

        public async Task<IEnumerable<TopUp>> TopUpsForCurrentMonth(int beneficiaryId)
        {
            return await _topUps.Where(x => x.BeneficiaryId == beneficiaryId && x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).ToListAsync();
        }
    }
}

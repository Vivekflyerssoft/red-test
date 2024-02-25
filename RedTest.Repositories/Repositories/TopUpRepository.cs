using Microsoft.EntityFrameworkCore;
using RedTest.Shared.Entities;
using RedTest.Shared.Repositories;

namespace RedTest.Repositories.Repositories
{
    public class TopUpRepository : RepositoryBase, ITopUpRepository
    {
        private readonly DbSet<TopUp> _topUps;
        private readonly DbSet<User> _users;

        public TopUpRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _topUps = dbContext.TopsUps;
            _users = dbContext.Users;
         }

        public async Task<bool> Add(TopUp topUp)
        {
            topUp.DateCreated = DateTime.UtcNow;
            _topUps.Add(topUp);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<bool> TopUp(User user, IEnumerable<TopUp> topUpList)
        {
            int totalTopUpAmount = (int)topUpList.Sum(x => x.Amount);
            user.Balance -= totalTopUpAmount;
            _users.Update(user);

            await _topUps.AddRangeAsync(topUpList);
            var result = await _dbContext.SaveChangesAsync();
            return result > 0;
        }

        public async Task<IEnumerable<TopUp>> TopUpsForCurrentMonth(int beneficiaryId)
        {
            return await _topUps.Where(x => x.BeneficiaryId == beneficiaryId && x.DateCreated.Month == DateTime.Now.Month && x.DateCreated.Year == DateTime.Now.Year).ToListAsync();
        }
    }
}

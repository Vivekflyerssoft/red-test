using Microsoft.EntityFrameworkCore;
using RedTest.Shared.Entities;
using RedTest.Shared.Repositories;

namespace RedTest.Repositories
{
    public class BeneficiaryRepository : RepositoryBase, IBeneficiariesRepository 
    {
        private readonly DbSet<Beneficiary> _beneficiaries;

        public BeneficiaryRepository(ApplicationDbContext dbContext): base(dbContext)
        {
            _beneficiaries = dbContext.Beneficiaries;
        }

        public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesFor(int userId)
        {
            return await _beneficiaries.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<bool> Add(Beneficiary beneficiary)
        {
            await _beneficiaries.AddAsync(beneficiary);
            int result = await _dbContext.SaveChangesAsync();
            return result == 0;
        }
    }
}

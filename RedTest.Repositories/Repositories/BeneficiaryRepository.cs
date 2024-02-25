using Microsoft.EntityFrameworkCore;
using RedTest.Repositories.Repositories;
using RedTest.Shared;
using RedTest.Shared.Entities;
using RedTest.Shared.Repositories;

namespace RedTest.Repositories
{
    public class BeneficiaryRepository : RepositoryBase, IBeneficiaryRepository
    {
        private readonly DbSet<Beneficiary> _beneficiaries;

        public BeneficiaryRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _beneficiaries = dbContext.Beneficiaries;
        }

        public async Task<IEnumerable<Beneficiary>> GetAllBeneficiariesFor(int userId)
        {
            return await _beneficiaries.Where(x => x.UserId == userId).ToListAsync();
        }

        public async Task<Result<Beneficiary>> Add(Beneficiary beneficiary)
        {
            await _beneficiaries.AddAsync(beneficiary);
            int result = await _dbContext.SaveChangesAsync();
            return result > 0 ? ResultFactory.Success(beneficiary) : ResultFactory.Error<Beneficiary>("Add beneficiary failed");
        }
    }
}

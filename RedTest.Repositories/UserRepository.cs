using Microsoft.EntityFrameworkCore;
using RedTest.Shared.Entities;
using RedTest.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTest.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = _dbContext.Users;
        }

        public async Task<IEnumerable<Beneficiary>> GetBeneficiaries(int id)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Id == id);
            return user?.Beneficiaries ?? Enumerable.Empty<Beneficiary>();
        }
    }
}

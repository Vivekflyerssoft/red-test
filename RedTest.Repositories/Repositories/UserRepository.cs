using Microsoft.EntityFrameworkCore;
using RedTest.Shared;
using RedTest.Shared.Entities;
using RedTest.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTest.Repositories.Repositories
{
    public class UserRepository : RepositoryBase, IUserRepository
    {
        private readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            _users = _dbContext.Users;
        }

        public async Task<bool> IsUserExist(string name)
        {
            return await _users.AnyAsync(x => x.Name == name);
        }

        public async Task<bool> IsUserExist(int id)
        {
            return await _users.AnyAsync(x => x.Id == id);
        }

        public async Task<Result<User>> FindBy(int userId)
        {
            var dbResult = await _users.FirstOrDefaultAsync(x => x.Id == userId);
            if (dbResult == null)
            {
                return ResultFactory.Error<User>("User not found");
            }
            return ResultFactory.Success(dbResult);
        }

        public async Task<Result<IEnumerable<Beneficiary>>> GetBeneficiaries(int id)
        {
            var user = await _users.FirstOrDefaultAsync(u => u.Id == id);
            return ResultFactory.Success(user?.Beneficiaries ?? Enumerable.Empty<Beneficiary>());
        }

        public async Task<Result<User>> Create(User user)
        {
            if (await IsUserExist(user.Name))
            {
                return ResultFactory.Error<User>("User already present.");
            }

            var result = _users.Add(user);
            return _dbContext.SaveChanges() > 0 ? ResultFactory.Success(result.Entity) : ResultFactory.Error<User>("Unable to create user.");
        }

        public async Task<Result<User>> Update(User user)
        {
            if (await IsUserExist(user.Name))
            {
                return ResultFactory.Error<User>("User already present.");
            }

            var result = _users.Update(user);
            return _dbContext.SaveChanges() > 0 ? ResultFactory.Success(result.Entity) : ResultFactory.Error<User>("Unable to update balance amount user.");
        }


    }
}

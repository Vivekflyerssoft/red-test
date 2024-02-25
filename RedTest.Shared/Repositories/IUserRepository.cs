using RedTest.Shared.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTest.Shared.Repositories
{
    public interface IUserRepository
    {
        Task<Result<User>> Create(User user);
        Task<Result<User>> FindBy(int userId);
        Task<Result<IEnumerable<Beneficiary>>> GetBeneficiaries(int id);
        Task<bool> IsUserExist(string name);
        Task<bool> IsUserExist(int id);
        Task<Result<User>> Update(User user);
    }
}

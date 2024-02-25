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
        Task<IEnumerable<Beneficiary>> GetBeneficiaries(int id);
    }
}

using RedTest.Shared.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RedTest.Shared.Services
{
    public interface IUserService
    {
        Task<Result<UserDTO>> Create(UserDTO user);
    }
}

using RedTest.Shared;
using RedTest.Shared.DTOs;
using RedTest.Shared.Repositories;
using RedTest.Shared.Services;

namespace RedTest.Domain.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            this._userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<Result<UserDTO>> Create(UserDTO user)
        {
            var dbResult = await _userRepository.Create(new Shared.Entities.User { Name = user.Name });

            if (!dbResult.IsSuccess)
            {
                return ResultFactory.Error<UserDTO>(dbResult.ErrorMessage);
            }

            Shared.Entities.User? data = dbResult.Data;
            return ResultFactory.Success(new UserDTO { Id = data.Id, Name = data.Name });
        }
    }
}

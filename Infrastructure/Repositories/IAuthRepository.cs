using Application.DTO;

namespace Application.Repositories
{
    public interface IAuthRepository
    {
        public Task<AddUserDto?> RegisterUserAsync(AddUserDto addUserDto);
        public Task<string?>LoginUserAsync(LoginUserDto loginUserDto);
    }
}

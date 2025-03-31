using Application.DTO;
using ApplicationDTO;
using Domain.Entities;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> GetAllUsersAsync();
       // public Task<string?> AddUserAync(AddUserDto addUserDto);
        public Task<User?> GetUserByIdAync(Guid id);
        public Task<User?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto);
        public Task<string?> DeleteUserAsync(Guid id);


    }
}

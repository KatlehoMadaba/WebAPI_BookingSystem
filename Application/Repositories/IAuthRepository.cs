using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Repositories
{
    public interface IAuthRepository
    {
        public Task<AddUserDto?> RegisterUserAsync(AddUserDto addUserDto);
        public Task<string?>LoginUserAsync(LoginUserDto loginUserDto);
    }
}

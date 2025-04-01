using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Services
{
    interface IUserAppServices
    {
        public Task<AddUserDto?> RegisterUserAsync(AddUserDto addUserDto);
        public Task<string?> LoginUserAsync(LoginUserDto loginUserDto);
    }
}

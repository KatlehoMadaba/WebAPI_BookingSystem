using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Repositories
{
    interface IAuthRepository
    {
        public Task<AddUserDto?> Register(AddUserDto adduserDto);

    }
}

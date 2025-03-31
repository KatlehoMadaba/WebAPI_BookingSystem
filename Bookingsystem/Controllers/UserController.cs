using Application.DTO;
using Application.Repositories;
using ApplicationDTO;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Bookingsystem.Controllers
{
    //localhost:xxx/api/user
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper mapper;
        private readonly IUserRepository userRepository;

        //Injectng your IUserRepo and Mapper
        public UserController( IMapper mapper,IUserRepository userRepository)
        {
            this.mapper = mapper;
            this.userRepository = userRepository;

        }

        [HttpGet ("GetAllUser",Name ="Get All Users")]
        public async Task<IActionResult> GetAllUsersAsync()
        {

            //var allUsers = await dbContext.Users.ToListAsync();
            var allUsers = await userRepository.GetAllUsersAsync();

            var userDTOData = mapper.Map<List<AddUserDto>>(allUsers);

            return Ok(userDTOData);
        }
        //public async Task<IActionResult> AddUserAsync(AddUserDto addUserDto)
        //{

        //    if (addUserDto == null)
        //    {
        //        return BadRequest();
        //    }

        //    await userRepository.AddUserAync(addUserDto);

        //    //User user = mapper.Map<User>(addUserDto);
        //    //await dbContext.Users.AddAsync(user);
        //    //await dbContext.SaveChangesAsync();
        //    return Ok();

        //}

        [HttpGet("GetUserById/{id:guid}", Name = "GetUserById")]
        public async Task<IActionResult> GetUserByIdAync(Guid id)
        {
            //var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            var user = await userRepository.GetUserByIdAync(id);

            if (user is null)
            {
                return NotFound(new { message = " Bad Request User Id Not Found" });
            }

            var userDTOData = mapper.Map<AddUserDto>(user);

            return Ok(userDTOData);
        }

        [HttpGet("GetUserByName/{name:alpha}", Name ="GetUserByName")]
        public async Task<IActionResult>GetUserByNameAsync(string name)
        {
            var userName= await userRepository.GetUserByUserNameAsync(name);
            if (userName is null) 
            {
                return BadRequest();
            }
            return Ok(userName);
        }

        [HttpPut("UpdateUserById/{id:guid}",Name ="UpdateUser")]
        public async Task<IActionResult> UpdateUserAsync( Guid id, UpdateUserDto updateUserDto)
            {
            if (updateUserDto == null) 
            {
                return BadRequest(); 
            }
            //var exsitingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            var exsitingUser= await userRepository.GetUserByIdAync(id);

            if (exsitingUser is null)
            {
                return NotFound(new { message = "User Not Found" });
            }
            mapper.Map(updateUserDto, exsitingUser);

            //dbContext.Users.Update(exsitingUser);
            //await dbContext.SaveChangesAsync();
            //return NoContent();

            await userRepository.UpdateUserAsync(id, updateUserDto);
            return NoContent();
        }

        [HttpDelete]
        [Route("DeleteUserById/{id:guid}",Name ="DeleteUser")]
        public async Task<IActionResult> DeleteUserAync(Guid id)
        {
            //var exsitingUser = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            var exsitingUser = await userRepository.GetUserByIdAync(id);

            if (exsitingUser is null)
            {
                return NotFound();
            }
            //dbContext.Users.Remove(exsitingUser);
            //await dbContext.SaveChangesAsync();

            await userRepository.DeleteUserAsync(id);
            return Ok();
        }

    }

}

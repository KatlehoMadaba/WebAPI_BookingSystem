using Application.DTO;
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
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        //Injectng your db context
        public UserController(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync()
        {

            var allUsers = await dbContext.Users.ToListAsync();

            var userDTOData = mapper.Map<List<AddUserDto>>(allUsers);

            return Ok(userDTOData);
        }

        [HttpPost]


        public async Task<IActionResult> AddUserAsync(AddUserDto addUserDto)
        {

            if (addUserDto == null)
            {

                return BadRequest();
            }
            User user = mapper.Map<User>(addUserDto);

            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();


            return Ok();

        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAync(Guid id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);

            if (user is null)
            {
                return NotFound(new { message = " Bad Request User Id Not Found" });
            }

            var userDTOData = mapper.Map<AddUserDto>(user);

            return Ok(userDTOData);
        }


        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            if (updateUserDto == null)
                BadRequest();

            var exsitingUser = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (exsitingUser is null)
            {
                return NotFound(new { message = "User Not Found" });
            }
            mapper.Map(updateUserDto, exsitingUser);


            dbContext.Users.Update(exsitingUser);

            await dbContext.SaveChangesAsync();
            return NoContent();

        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUserAync(Guid id)
        {
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return NotFound();
            }

            dbContext.Users.Remove(user);
            await dbContext.SaveChangesAsync();
            return Ok();
        }

    }

}

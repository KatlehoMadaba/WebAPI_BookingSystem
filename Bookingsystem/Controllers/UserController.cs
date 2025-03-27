using AutoMapper;
using Bookingsystem.Data;
using Bookingsystem.Models.DTO;
using Bookingsystem.Models.Entities;
using Microsoft.AspNetCore.Http;
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

        [HttpGet]//Deco
        //connecting to the database and return the list of the employees back 
        public async Task<IActionResult> GetAllUsersAsync()
        {
            //Before DTO and auto map DTO you use to give the user everything including the id:

            //var allUsers = await dbContext.Users.ToListAsync();
            ////return 200 response//with the id
            //return Ok(allUsers);


            //return users as a list
            var allUsers = await dbContext.Users.ToListAsync();

            //<Destination class>(source class)
            //So how it works is that you send the list of all users to AddUserDto which is basically your User class that contains everything in your User class
            //Now your AddUserDto class has its own properties that are sort of similiar to the User class but does have everything because you are trying to hide certain information from your user
            //Now instead of sending the properties from User class to UpdateUserDto manually you use mapper to do it for you automatically

            var userDTOData = mapper.Map<List<AddUserDto>>(allUsers);

            //return 200 response//without the id 
            return Ok(userDTOData);
        }

        [HttpPost]

        //DTO=>Data transfer objects =>Transer the data from one operation to another
        public async Task<IActionResult> AddUserAsync(AddUserDto addUserDto)
        {
            //want save this user in the database table
            //Before mapper
            //var userEntity = new User()
            //{
            //    Name = addUserDto.Name,
            //    Email = addUserDto.Email,
            //    Cellphone = addUserDto.Cellphone
            //};
            //await dbContext.Users.AddAsync(userEntity);


            //checking id the DTO you are sending is empty or not 
            if (addUserDto == null)
            {

                return BadRequest();
            }
            User user = mapper.Map<User>(addUserDto);

            /*Context.Users.Add(userEntity);*/
            await dbContext.Users.AddAsync(user);
            await dbContext.SaveChangesAsync();

            //usally its a 201
            //return Created();
            return CreatedAtAction(nameof(AddUserAsync), user);

            //if you want to return something specific to the user from the dto class:
            //return CreatedAtRoute("GetUserById",new { id = addUserDto.Id},addUserDto);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public async Task<IActionResult> GetUserByIdAync(Guid id)
        {
            //var user = (from u in dbContext.Users
            //            where u.Id == id
            //            select u).FirstOrDefault();


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

            //checking if the user exists
            var exsitingUser = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
            if (exsitingUser is null)
            {
                //Learn the status codes
                return NotFound(new { message = "User Not Found" });
            }

            //Before auto mapping 
            //user.Name = updateUserDto.Name;
            //user.Email = updateUserDto.Email;
            //user.Cellphone = updateUserDto.Cellphone;

            //The destination is User because we are Updating the user class/User Table
            //var newRecord = mapper.Map<>(updateUserDto);

            //Taking new data from updateUserDto and replacing what is in exsiting user where the properties match 
            //after this runs existingUser is updated thats why we can pass it into the dbContext.Users.Update(exsitingUser)
            mapper.Map(updateUserDto, exsitingUser);

            //Updates changes in the database 
            dbContext.Users.Update(exsitingUser);
            //Saves the changes you updated 
            await dbContext.SaveChangesAsync();
            return NoContent();
            //return Ok(updateUserDto);
        }
        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeleteUserAync(Guid id)
        {
            var user =  await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
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

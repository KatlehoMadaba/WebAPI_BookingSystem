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

        //Injectng your db context
        public UserController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]//Deco
        //connecting to the database and return the list of the employees back 
        public async Task<IActionResult> GetAllUsers()
        {
            var allUsers = await dbContext.Users.ToListAsync();
            //return 200 response
            return Ok(allUsers);
        }
        [HttpPost]

        //DTO=>Data transfer objects =>Transer the data from one operation to another
        public async Task<IActionResult> AddUser(AddUserDto addUserDto)
        {
            //want save this user in the database table
            var userEntity = new User()
            {
                Name = addUserDto.Name,
                Email = addUserDto.Email,
                Cellphone = addUserDto.Cellphone
            };

            /*Context.Users.Add(userEntity);*/
             await dbContext.Users.AddAsync(userEntity);
             await dbContext.SaveChangesAsync();

            //usally its a 201
            //return Created();
            return CreatedAtAction(nameof(AddUser), userEntity);
        }

        [HttpGet]
        [Route("{id:guid}")]
        public  async Task <IActionResult> GetUserById(Guid id)
        {
            var user =  await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
            if (user is null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public  async Task <IActionResult> UpdateUser(Guid id,UpdateUserDto updateUserDto)
        {
            //var user=dbContext.Users.Find(id);

            var user = await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);

            //var user = dbContext.Users;

            //var user = (from u in dbContext.Users
            //            where u.Id == id
            //            select u).FirstOrDefault();



            if (user is null)
            {
                //Learn the status codes
                return NotFound();
            }
            user.Name = updateUserDto.Name;
            user.Email = updateUserDto.Email;
            user.Cellphone = updateUserDto.Cellphone;
            
            await dbContext.SaveChangesAsync();  
            return Ok(user);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public IActionResult DeleteUser(Guid id)
        { 
            var user =dbContext.Users.Find(id);
            if(user is null)
            {
                return NotFound();
            }
                dbContext.Users.Remove(user);
                dbContext.SaveChanges();
                return Ok();
        }

    }

}

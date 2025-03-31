using Application.DTO;
using ApplicationDTO;
using AutoMapper;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
namespace Application.Repositories
{
    public class UserRespository : IUserRepository
    {
        private readonly ApplicationDbContext dbContext;
        private readonly IMapper mapper;

        //Injectng your db context and your mapper 
        public UserRespository(ApplicationDbContext dbContext, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.mapper = mapper;
        }

        //public async Task<string?>AddUserAync(AddUserDto addUserDto)
        //{
        //    if (addUserDto == null) 
        //    { 
        //        return null;
        //    }

        //    User newuser=mapper.Map<User>(addUserDto);

        //    await dbContext.Users.AddAsync(newuser);

        //    await dbContext.SaveChangesAsync();

        //    return "user Added";
        //}
        public async Task<User?> GetUserByIdAync(Guid id)
        {

            return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> UpdateUserAsync(Guid id, UpdateUserDto updateUserDto)
        {
            var exstingUser = await GetUserByIdAync(id);
            //checking if user exists 
            if (exstingUser == null)
            {
                return null;
            }

            //updating(mapping) the existing user with the information coming from the dto
            mapper.Map(updateUserDto , exstingUser);
            
            //Updating the database with user object that has been updated 
            dbContext.Users.Update(exstingUser);

            //Saving the changes 
            await dbContext.SaveChangesAsync();

            //return the updated user:
            return exstingUser;
            //return the updated user:
            //return await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id);
        }
        public async Task<string?> DeleteUserAsync(Guid id)
        {

            var existingUser = await GetUserByIdAync(id);

            if (existingUser == null)
            {
                return null;
            }

            dbContext.Users.Remove(existingUser);
            await dbContext.SaveChangesAsync();
            return "deleted";
        }
    }
}

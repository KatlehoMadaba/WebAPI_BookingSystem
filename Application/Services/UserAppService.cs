using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Application.DTO;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Application.Services
{
    class UserAppService:IUserAppServices
    {
        public async Task<AddUserDto?> RegisterUserAsync(AddUserDto addUserDto)
        {
            bool userExists = await dbContext.Users.AnyAsync(u => u.Email == addUserDto.Email);

            if (userExists)
            {
                return null;
            }
            User newuser = mapper.Map<User>(addUserDto);

            var hashedPassword = new PasswordHasher<User>().HashPassword(newuser, addUserDto.Password);

            newuser.HashedPassword = hashedPassword;

            //mapper is used to convert objects not indiviual objects
            //mapper.Map(hashedPassword, password);


            await dbContext.Users.AddAsync(newuser);
            await dbContext.SaveChangesAsync();
            return mapper.Map<AddUserDto>(newuser);

        }
        public async Task<string?> LoginUserAsync(LoginUserDto loginUserDto)
        {
            //finding the user with that spefic email
            var user = await dbContext.Users.FirstOrDefaultAsync(u => u.Email == loginUserDto.Email);

            if (user == null)
            {
                return null;
            }

            //var Hashedpassword= await dbContext.Users.FirstOrDefaultAsync(p=>p.HashedPassword==loginUserDto.Password);
            var passwordVerfied = new PasswordHasher<User>().VerifyHashedPassword(user, user.HashedPassword, loginUserDto.Password) == PasswordVerificationResult.Success;

            if (!passwordVerfied)
            {
                return null;
            }
            ////before hashing
            //var user=await dbContext.Users.FirstOrDefaultAsync(u=>u.Email == loginUserDto.Email && u.HashedPassword==loginUserDto.Password);

            //if (user == null) 
            //{ 
            //    return null;
            //}

            return CreateToken(user);
        }
        //helper function 
        private string CreateToken(User user)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim (ClaimTypes.NameIdentifier, user.Id.ToString()),
                //new Claim(ClaimTypes.Role, user.Role),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Token!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: jwtSettings.Issuer,
                audience: jwtSettings.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddDays(1),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

        }
    }
}

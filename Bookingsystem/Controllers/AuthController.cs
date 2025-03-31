using Application.DTO;
using Application.Repositories;
using AutoMapper;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Bookingsystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthRepository authRepository;

        public AuthController(ApplicationDbContext dbContext, IMapper mapper, IAuthRepository authRepository)
        {
            this.authRepository = authRepository;

        }
        [HttpPost("register")]
        public async Task<IActionResult> RegisterUserAsync([FromBody] AddUserDto addUserDto)
        {

            if (addUserDto == null)
            {
                return BadRequest();
            }

            await authRepository.RegisterUserAsync(addUserDto);

            return Ok();

        }

        [HttpPost("login")]
        public async Task<IActionResult> LoginUserAsync([FromBody] LoginUserDto loginUserDto)
        {
            var result=await authRepository.LoginUserAsync(loginUserDto);

            if (result is null)
            {
                return BadRequest();
            }

            return Ok(result);
        }
    }
}

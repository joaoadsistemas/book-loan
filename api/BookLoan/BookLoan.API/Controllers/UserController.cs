using BookLoan.API.Models;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Domain.Account;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLoan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IAuthenticateRepository _authenticateRepository;

        public UserController(IUserService userService, IAuthenticateRepository authenticateRepository)
        {
            _userService = userService;
            _authenticateRepository = authenticateRepository;
        }

        [HttpPost("register")]
        public async Task<ActionResult<UserToken>> Insert([FromBody] UserDTO userDto)
        {
            if (userDto == null)
            {
                return BadRequest("Invalid data");
            }

            var existEmail = await _authenticateRepository.VerifyUserExistsByEmail(userDto.Email);

            if (existEmail)
            {
                return BadRequest("This email already exists in this system");
            }

            UserDTO user = await _userService.Insert(userDto);
            if (user == null)
            {
                return BadRequest("An error occurred while register user");
            }

            var token = _authenticateRepository.GenerateToken(user.Id, user.Email);
            var userToken = new UserToken()
            {
                Token = token
            };

            return Ok(userToken);

        }

        [HttpPost("login")]
        public async Task<ActionResult<UserToken>> Login([FromBody] LoginModel loginModel)
        {

            var exist = await _authenticateRepository.VerifyUserExistsByEmail(loginModel.Email);

            if (!exist)
            {
                return Unauthorized("This user does not exists");
            }

            var result = await _authenticateRepository.AuthenticateAsync(loginModel.Email, loginModel.Password);
            if (!result)
            {
                return Unauthorized("Invalid data");
            }

            var user = await _authenticateRepository.GetUserByEmail(loginModel.Email);

            var token = _authenticateRepository.GenerateToken(user.Id, user.Email);

            var userToken = new UserToken() { Token = token };

            return Ok(userToken);

        }


    }
}

using BookLoan.API.Extensions;
using BookLoan.API.Models;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Domain.Account;
using BookLoan.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
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



        [HttpGet]
        [Authorize]
        public async Task<ActionResult> FindAll([FromQuery] PaginationParams paginationParams)
        {
            var userId = User.GetId();
            var userLogged = await _userService.FindById(userId);

            if (!userLogged.IsAdmin)
            {
                return Unauthorized("You dont have permission for search system users.");
            }

            var users = await _userService.FindAll(paginationParams.PageNumber, paginationParams.PageSize);
            Response.AddPaginationHeader(new PaginationHeader(paginationParams.PageNumber, users.PageSize,
                users.TotalCount, users.TotalPages));
            return Ok(users);
        }

        [HttpGet("filter")]
        [Authorize]
        public async Task<ActionResult> FindByFilter([FromQuery] UserFilter userFilter)
        {
            var userId = User.GetId();
            var userLogged = await _userService.FindById(userId);

            if (!userLogged.IsAdmin)
            {
                return Unauthorized("Você não tem permissão para consultar os usuários do sistema.");
            }

            var users = await _userService.FindByFilter(userFilter.Name, userFilter.Email,
                userFilter.IsAdmin, userFilter.IsNotAdmin, userFilter.Active, userFilter.Inactive, userFilter.PageNumber, userFilter.PageSize);
            Response.AddPaginationHeader(new PaginationHeader(userFilter.PageNumber, users.PageSize,
                users.TotalCount, users.TotalPages));
            return Ok(users);
        }


        [HttpGet("{id}")]
        [Authorize]
        public async Task<ActionResult> FindById(int id)
        {
            var userId = User.GetId();
            var userLogged = await _userService.FindById(userId);

            if (id == 0)
            {
                id = userId;
            }

            if (!userLogged.IsAdmin && userLogged.Id != id)
            {
                return Unauthorized("You dont have permission for search system users.");
            }

            var user = await _userService.FindById((int)id);
            return Ok(user);
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

            var existUserSystem = await _userService.ExistsUserRegistered();

            if (!existUserSystem)
            {
                userDto.IsAdmin = true;
            }
            else
            {
                if (User.FindFirst("id") == null)
                {
                    return Unauthorized();
                }

                var userId = User.GetId();
                var userSelected = await _userService.FindById(userId);

                if (!userSelected.IsAdmin)
                {
                    return Unauthorized("You dont have permission for include new user");
                }

            }

            UserDTO user = await _userService.Insert(userDto);
            if (user == null)
            {
                return BadRequest("An error occurred while register user");
            }

            var token = _authenticateRepository.GenerateToken(user.Id, user.Email);
            var userToken = new UserToken()
            {
                Token = token,
                Email = userDto.Email,
                IsAdmin = userDto.IsAdmin
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

            var userToken = new UserToken()
            {
                Token = token,
                IsAdmin = user.IsAdmin,
                Email = user.Email
            };

            return Ok(userToken);

        }


        [HttpPut]
        [Authorize]
        public async Task<ActionResult> Alterar(UserUpdateDTO userUpdatetDTO)
        {
            var userId = User.GetId();
            var userLogged = await _userService.FindById(userId);


            if (!userLogged.IsAdmin && userUpdatetDTO.Id != userId)
            {
                return Unauthorized("You dont have permission for update a user");
            }

            if (!userLogged.IsAdmin && userUpdatetDTO.Id == userId && userUpdatetDTO.IsAdmin)
            {
                return Unauthorized("You dont have permission for set yourself as admin.");
            }
            var usuario = await _userService.Update(userUpdatetDTO);

            return Ok(new { message = "User updated successfully!" });
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult> Excluir(int id)
        {
            var userId = User.GetId();
            var userLogged = await _userService.FindById(userId);

            if (!userLogged.IsAdmin)
            {
                return Unauthorized("You dont have permission for exclude a user");
            }

            var user = await _userService.Delete(id);
            return Ok(user);
        }


    }
}

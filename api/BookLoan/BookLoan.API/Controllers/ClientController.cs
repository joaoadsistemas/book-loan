using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Domain.Entities;
using BookLoan.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLoan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {

        private readonly IClientService _clientService;
        private readonly IUserService _userService;

        public ClientController(IClientService clientService, IUserService userService)
        {
            _clientService = clientService;
            _userService = userService;
        }


        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            return Ok(await _clientService.FindAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            try
            {
                return Ok(await _clientService.FindById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] ClientDTO clientDto)
        {
            ClientDTO client = await _clientService.Insert(clientDto);
            if (client == null)
            {
                return BadRequest("An error occurred while saving the client");
            }
            return Ok(client);
        }


        [HttpPut]
        public async Task<ActionResult> Update(ClientDTO clientDto)
        {
            ClientDTO client = await _clientService.Update(clientDto);
            if (client == null)
            {
                return BadRequest("An error occurred while changed the client");
            }
            return Ok(client);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {
                var userId = User.GetId();
                var user = await _userService.FindById(userId);

               if (user.IsAdmin)
               {
                   ClientDTO client = await _clientService.Delete(id);
                   return Ok("Client was removed");
               }

               return Unauthorized("Only admin can do this");

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}

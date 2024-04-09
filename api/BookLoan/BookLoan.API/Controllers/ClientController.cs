using BookLoan.API.Extensions;
using BookLoan.API.Models;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Application.Services;
using BookLoan.Domain.Entities;
using BookLoan.Infra.Ioc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLoan.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
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
        public async Task<ActionResult> FindAll([FromQuery] PaginationParams paginationParams)
        {
            var clientsDto = await _clientService.FindAll(paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(clientsDto.CurrentPage,
                clientsDto.PageSize, clientsDto.TotalCount, clientsDto.TotalPages));

            return Ok(clientsDto);
        }


        [HttpGet("filter")]
        public async Task<ActionResult> FindByFilter([FromQuery] ClientFilter clientFilter)
        {
            var clientDTO = await _clientService.FindByFilter
            (clientFilter.Cpf, clientFilter.Name, clientFilter.City,
                clientFilter.Neighborhood, clientFilter.PhoneNumber,
                clientFilter.FixPhoneNumber, clientFilter.PageNumber, clientFilter.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(clientDTO.CurrentPage,
                clientDTO.PageSize, clientDTO.TotalCount, clientDTO.TotalPages));

            return Ok(clientDTO);
        }

        [HttpGet("search")]
        public async Task<ActionResult> FindBySearch([FromQuery] TermSearch termSearch)
        {
            var clientDTO = await _clientService.FindByFilter
                (termSearch.Term, termSearch.PageNumber, termSearch.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(clientDTO.CurrentPage,
                clientDTO.PageSize, clientDTO.TotalCount, clientDTO.TotalPages));

            return Ok(clientDTO);
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
                   return Ok(client);
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

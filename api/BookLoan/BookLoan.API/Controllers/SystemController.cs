using BookLoan.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLoan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SystemController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ISystemService _systemService;

        public SystemController(IUserService userService, ISystemService systemService)
        {
            _userService = userService;
            _systemService = systemService;
        }

        [HttpGet("VerifyFirstUse")]
        public async Task<ActionResult> PrimeiroUso()
        {
            var existsUserRegistered = await _userService.ExistsUserRegistered();

            return Ok(new { firstTime = !existsUserRegistered });
        }

        [HttpGet("Dashboard")]
        [Authorize]
        public async Task<ActionResult> Dashboard()
        {
            var qtdItemDTO = await _systemService.SelectItemAmount();
            return Ok(qtdItemDTO);
        }
    }
}

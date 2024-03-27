using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BookLoan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanBookController : ControllerBase
    {


        private readonly ILoanBookService _loanBookService;

        public LoanBookController(ILoanBookService livroEmprestadoService)
        {
            _loanBookService = livroEmprestadoService;
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            await _loanBookService.Delete(id);
            return Ok(new { message = "Book was removed for loan successfully!" });
        }

        [HttpPost]
        public async Task<ActionResult> Insert(LoanBookDTO loanBookDTO)
        {
            await _loanBookService.Insert(loanBookDTO);
            return Ok(new { message = "Book was include in loan successfully!" });
        }

        [HttpGet("loan/{id}")]
        public async Task<ActionResult> FindAllByLoan(int id)
        {
            var loanBooks = await _loanBookService.FindAllByLoan(id);
            return Ok(loanBooks);
        }

    }
}

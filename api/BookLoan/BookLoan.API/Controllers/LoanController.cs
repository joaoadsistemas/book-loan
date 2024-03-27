using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanLoan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {

        private readonly ILoanService _loanService;

        public LoanController(ILoanService loanService)
        {
            _loanService = loanService;
        }


        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            return Ok(await _loanService.FindAll());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            try
            {
                return Ok(await _loanService.FindById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] LoanInsertDTO loanInsertDto)
        {
            loanInsertDto.LoanDate = DateTime.Now;
            loanInsertDto.Delivered = false;
            LoanDTO loan = await _loanService.Insert(loanInsertDto);
            if (loan == null)
            {
                return BadRequest("An error occurred while saving the loan");
            }
            return Ok(loan);
        }


        [HttpPut]
        public async Task<ActionResult> Update(LoanUpdateDTO loanUpdateDto)
        {

            var loanDto = await _loanService.FindById(loanUpdateDto.Id);
            if (loanDto == null)
            {
                return NotFound("Loan not found");
            }


            loanDto.DeliveryDate = loanUpdateDto.DeliveryDate;
            loanDto.Delivered = loanUpdateDto.Delivered;


            LoanDTO loan = await _loanService.Update(loanDto);
            if (loan == null)
            {
                return BadRequest("An error occurred while changed the loan");
            }
            return Ok(loan);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {

                LoanDTO loan = await _loanService.Delete(id);
                return Ok("Client was removed");


            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }


    }
}

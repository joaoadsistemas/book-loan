﻿using BookLoan.API.Extensions;
using BookLoan.API.Models;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LoanLoan.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class LoanController : ControllerBase
    {

        private readonly ILoanService _loanService;
        private readonly ILoanBookService _loanBookService;

        public LoanController(ILoanService loanService, ILoanBookService loanBookService)
        {
            _loanService = loanService;
            _loanBookService = loanBookService;
        }


        [HttpGet]
        public async Task<ActionResult> FindAll([FromQuery] PaginationParams paginationParams)
        {
            var loanDTO = await _loanService.FindAll(paginationParams.PageNumber, paginationParams.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(paginationParams.PageNumber, 
                paginationParams.PageSize, loanDTO.TotalCount, loanDTO.TotalPages));

            return Ok(loanDTO);
        }


        [HttpGet("filter")]
        public async Task<ActionResult> FindByFilter([FromQuery] LoanFilter loanFilter)
        {
            var loanDTO = await _loanService.FindByFilter
            (loanFilter.Cpf, loanFilter.Name,
                loanFilter.LoanDateInitial, loanFilter.LoanDateFinal,
                loanFilter.DeliverDateInitial, loanFilter.DeliverDateFinal,
                loanFilter.Delivered, loanFilter.NotDelivered, loanFilter.PageNumber, loanFilter.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(loanFilter.PageNumber,
                loanFilter.PageSize, loanDTO.TotalCount, loanDTO.TotalPages));

            return Ok(loanDTO);
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
            var available = await _loanService.VerifyAvailable(loanInsertDto.idsBooks);
            if (!available)
            {
                return BadRequest("The book is not available for loan.");
            }

            loanInsertDto.LoanDate = DateTime.Now;
            loanInsertDto.Delivered = false;

            var loanInsertDTOFinished = await _loanService.Insert(loanInsertDto);
            if (loanInsertDTOFinished == null)
            {
                return BadRequest("An error occurred while inserting the loan.");
            }
            else
            {
                List<LoanBookDTO> loanBookDTOs = new List<LoanBookDTO>();
                LoanBookDTO loanBook;

                foreach (var loanBookId in loanInsertDto.idsBooks)
                {
                    loanBook = new LoanBookDTO
                    {
                        LoanId = loanInsertDTOFinished.Id, 
                        BookId = loanBookId 
                    };
                    loanBookDTOs.Add(loanBook);
                }

                
                await _loanBookService.InsertManyAsync(loanBookDTOs);
            }

            return Ok(new { message = "Loan successfully inserted!" });
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

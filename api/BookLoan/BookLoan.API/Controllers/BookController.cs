using BookLoan.API.Extensions;
using BookLoan.API.Models;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Application.Services;
using BookLoan.Domain.Entities;
using LoanLoan.Application.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLoan.API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }
        [HttpGet]
        public async Task<ActionResult> FindAll([FromQuery] PaginationParams paginationParams)
        {
            var bookDTO = await _bookService.FindAll(paginationParams.PageNumber, paginationParams.PageSize);
            Response.AddPaginationHeader(new PaginationHeader(paginationParams.PageNumber,
                paginationParams.PageSize, bookDTO.TotalCount, bookDTO.TotalPages));

            return Ok(bookDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> FindById(int id)
        {
            try
            {
                return Ok(await _bookService.FindById(id));
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<ActionResult> Insert([FromBody] BookDTO bookDto)
        {
            BookDTO book = await _bookService.Insert(bookDto);
            if (book == null)
            {
                return BadRequest("An error occurred while saving the book");
            }
            return Ok(book);
        }


        [HttpPut]
        public async Task<ActionResult> Update(BookDTO bookDto)
        {
            BookDTO book = await _bookService.Update(bookDto);
            if (book == null)
            {
                return BadRequest("An error occurred while changed the book");
            }
            return Ok(book);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {

            try
            {
                
                BookDTO book = await _bookService.Delete(id);
                return Ok("Client was removed");
                

            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("filter")]
        public async Task<ActionResult> FindByFilter([FromQuery] BookFilter bookFilter)
        {
            var booksDTO = await _bookService.FindByFilter(bookFilter.Name, bookFilter.Author,
                bookFilter.Publisher, bookFilter.YearOfPublication, bookFilter.Edition, bookFilter.PageNumber,
                bookFilter.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(bookFilter.PageNumber,
                bookFilter.PageSize, booksDTO.TotalCount, booksDTO.TotalPages));

            return Ok(booksDTO);
        }

        [HttpGet("search")]
        public async Task<ActionResult> FindBySearch([FromQuery] TermSearch termSearch)
        {
            var booksDTO = await _bookService.FindByFilter(termSearch.Term, termSearch.PageNumber, termSearch.PageSize);

            Response.AddPaginationHeader(new PaginationHeader(termSearch.PageNumber,
                termSearch.PageSize, booksDTO.TotalCount, booksDTO.TotalPages));

            return Ok(booksDTO);
        }

    }
}

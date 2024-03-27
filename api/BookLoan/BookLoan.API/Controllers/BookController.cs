using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Application.Services;
using BookLoan.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BookLoan.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {

        private readonly IBookService _bookService;

        public BookController(IBookService bookService)
        {
            _bookService = bookService;
        }

        [HttpGet]
        public async Task<ActionResult> FindAll()
        {
            return Ok(await _bookService.FindAll());
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

    }
}

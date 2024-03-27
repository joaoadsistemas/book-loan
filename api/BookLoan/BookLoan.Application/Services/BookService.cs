using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;

namespace BookLoan.Application.Services
{
    public class BookService : IBookService
    {

        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BookDTO>> FindAll()
        {
            IEnumerable<Book> books = await _bookRepository.FindAll();
            IEnumerable<BookDTO> bookDtos = _mapper.Map<IEnumerable<BookDTO>>(books);
            return bookDtos;

        }

        public async Task<BookDTO> FindById(int id)
        {
            Book book = await _bookRepository.FindById(id);
            BookDTO bookDto = _mapper.Map<BookDTO>(book);
            return bookDto;
        }

        public async Task<BookDTO> Insert(BookDTO bookDTO)
        {

            Book book = _mapper.Map<Book>(bookDTO);
            Book bookInserted = await _bookRepository.Insert(book);
            return _mapper.Map<BookDTO>(bookInserted);

        }

        public async Task<BookDTO> Update(BookDTO bookDTO)
        {
            Book book = _mapper.Map<Book>(bookDTO);
            Book bookChanged = await _bookRepository.Update(book);
            return _mapper.Map<BookDTO>(bookChanged);
        }

        public async Task<BookDTO> Delete(int id)
        {
            Book bookDeleted = await _bookRepository.Delete(id);
            return _mapper.Map<BookDTO>(bookDeleted);
        }
    }
}


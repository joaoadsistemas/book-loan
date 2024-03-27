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
    public class LoanBookService : ILoanBookService
    {

        private readonly ILoanBookRepository _loanBookRepository;
        private readonly IMapper _mapper;

        public LoanBookService(ILoanBookRepository repository, IMapper mapper)
        {
            _loanBookRepository = repository;
            _mapper = mapper;
        }

        public async Task<LoanBookDTO> Delete(int id)
        {
            var loanBook = await _loanBookRepository.Delete(id);
            return _mapper.Map<LoanBookDTO>(loanBook);
        }

        public async Task<LoanBookDTO> Insert(LoanBookDTO loanBookDTO)
        {
            var loanBook = _mapper.Map<LoanBook>(loanBookDTO);
            var loanBookInsert = await _loanBookRepository.Insert(loanBook);
            return _mapper.Map<LoanBookDTO>(loanBookInsert);
        }

        public async Task<IEnumerable<LoanBookDTO>> InsertManyAsync(IEnumerable<LoanBookDTO> loanBooksDTO)
        {
            var loanBooks = _mapper.Map<IEnumerable<LoanBook>>(loanBooksDTO);
            var loanBooksInsert = await _loanBookRepository.InsertManyAsync(loanBooks);
            return _mapper.Map<IEnumerable<LoanBookDTO>>(loanBooksInsert);
        }

        public async Task<LoanBookDTO> FindById(int id)
        {
            var loanBooks = await _loanBookRepository.FindById(id);
            return _mapper.Map<LoanBookDTO>(loanBooks);
        }

        public async Task<IEnumerable<LoanBookDTO>> FindAllByLoan(int id)
        {
            var loanBooks = await _loanBookRepository.FindAllByLoan(id);
            return _mapper.Map<IEnumerable<LoanBookDTO>>(loanBooks);
        }

        public async Task<IEnumerable<LoanBookDTO>> UpdateAllAsync(List<LoanBookDTO> loanBooksDTO)
        {
            var loanBooks = _mapper.Map<List<LoanBook>>(loanBooksDTO);
            var loanBooksNews = await _loanBookRepository.UpdateAllAsync(loanBooks);
            return _mapper.Map<IEnumerable<LoanBookDTO>>(loanBooksNews);
        }

    }
}

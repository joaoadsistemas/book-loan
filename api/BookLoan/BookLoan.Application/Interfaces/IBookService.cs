using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Pagination;

namespace BookLoan.Application.Interfaces
{
    public interface IBookService
    {

        Task<PagedList<BookDTO>> FindAll(int pageNumber, int pageSize);
        Task<BookDTO> FindById(int id);
        Task<BookDTO> Insert(BookDTO bookDTO);
        Task<BookDTO> Update(BookDTO bookDTO);
        Task<BookDTO> Delete(int id);
        Task<PagedList<BookDTO>> FindByFilter(
            string name, string author, string Publisher,
            DateTime? yearOfPublication, string edition, int pageNumber, int pageSize);
        Task<PagedList<BookDTO>> FindByFilter(string terms, int pageNumber, int pageSize);

    }
}

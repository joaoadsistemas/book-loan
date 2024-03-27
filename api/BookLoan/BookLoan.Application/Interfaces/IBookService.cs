using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;

namespace BookLoan.Application.Interfaces
{
    public interface IBookService
    {

        Task<IEnumerable<BookDTO>> FindAll();
        Task<BookDTO> FindById(int id);
        Task<BookDTO> Insert(BookDTO bookDTO);
        Task<BookDTO> Update(BookDTO bookDTO);
        Task<BookDTO> Delete(int id);

    }
}

using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Domain.Pagination;

namespace BookLoan.Domain.Interfaces
{
    public interface IBookRepository
    {

        Task<Book> Insert(Book book);
        Task<Book> Update(Book book);
        Task<Book> Delete(int id);
        Task<Book> FindById(int id);
        Task<PagedList<Book>> FindAll(int pageNumber, int pageSize);
        Task<PagedList<Book>> FindByFilter(
            string name, string author, string Publisher,
            DateTime? yearOfPublication, string edition, int pageNumber, int pageSize);
        Task<PagedList<Book>> FindByFilter(string terms, int pageNumber, int pageSize);

    }
}

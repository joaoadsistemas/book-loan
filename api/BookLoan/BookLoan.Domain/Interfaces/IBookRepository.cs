using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Domain.Interfaces
{
    public interface IBookRepository
    {

        Task<IEnumerable<Book>> FindAll();
        Task<Book> FindById(int id);
        Task<Book> Insert(Book book);
        Task<Book> Update(Book book);
        Task<Book> Delete(int id);
        Task<bool> SaveAllAsync();

    }
}

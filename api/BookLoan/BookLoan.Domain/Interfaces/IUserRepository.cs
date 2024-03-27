using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Domain.Pagination;

namespace BookLoan.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<PagedList<User>> FindAll(int pageNumber, int pageSize);
        Task<PagedList<User>> FindAllByFilterAsync(
            string name, string email, bool? isAdmin, bool? isNotAdmin, bool? active, bool? inactive, int pageNumber, int pageSize);
        Task<User> FindById(int id);
        Task<User> Insert(User user);
        Task<User> Update(User user);
        Task<bool> ExistsUserRegistered();
        Task<User> Delete(int id);
        Task<bool> SaveAllAsync();

    }
}

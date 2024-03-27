using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Domain.Interfaces
{
    public interface IUserRepository
    {

        Task<IEnumerable<User>> FindAll();
        Task<User> FindById(int id);
        Task<User> Insert(User user);
        Task<User> Update(User user);
        Task<User> Delete(int id);
        Task<bool> SaveAllAsync();

    }
}

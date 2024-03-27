using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;
using BookLoan.Domain.Pagination;

namespace BookLoan.Application.Interfaces
{
    public interface IUserService
    {

        Task<PagedList<UserDTO>> FindAll(int pageNumber, int pageSize);
        Task<PagedList<UserDTO>> FindByFilter(string name, string email, bool? isAdmin, bool? isNotAdmin, bool? active, bool? inactive, int pageNumber, int pageSize);
        Task<UserDTO> FindById(int id);
        Task<UserDTO> Insert(UserDTO userDTO);
        Task<UserUpdateDTO> Update(UserUpdateDTO userUpdateDTO);
        Task<bool> ExistsUserRegistered();
        Task<UserDTO> Delete(int id);

    }
}

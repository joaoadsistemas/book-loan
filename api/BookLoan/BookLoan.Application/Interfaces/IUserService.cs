using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;

namespace BookLoan.Application.Interfaces
{
    public interface IUserService
    {

        Task<IEnumerable<UserDTO>> FindAll();
        Task<UserDTO> FindById(int id);
        Task<UserDTO> Insert(UserDTO userDTO);
        Task<UserDTO> Update(UserDTO userDTO);
        Task<UserDTO> Delete(int id);

    }
}

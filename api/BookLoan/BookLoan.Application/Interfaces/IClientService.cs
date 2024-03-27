using BookLoan.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;

namespace BookLoan.Application.Interfaces
{
    public interface IClientService
    {

        Task<IEnumerable<ClientDTO>> FindAll();
        Task<ClientDTO> FindById(int id);
        Task<ClientDTO> Insert(ClientDTO clientDTO);
        Task<ClientDTO> Update(ClientDTO clientDTO);
        Task<ClientDTO> Delete(int id);

    }
}

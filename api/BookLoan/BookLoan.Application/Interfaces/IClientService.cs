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
    public interface IClientService
    {

        Task<PagedList<ClientDTO>> FindAll(int pageNumber, int pageSize);
        Task<PagedList<ClientDTO>> FindByFilter(string cpf, string name,
            string city, string neighbourhood, string phoneNumber, string fixPhoneNumber,
            int pageNumber, int pageSize);

        Task<PagedList<ClientDTO>> FindByFilter(string term, int pageNumber, int pageSize);
        Task<ClientDTO> FindById(int id);
        Task<ClientDTO> Insert(ClientDTO clientDTO);
        Task<ClientDTO> Update(ClientDTO clientDTO);
        Task<ClientDTO> Delete(int id);

    }
}

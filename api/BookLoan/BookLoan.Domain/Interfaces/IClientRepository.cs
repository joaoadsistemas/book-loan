using BookLoan.Domain.Entities;
using BookLoan.Domain.Pagination;

namespace BookLoan.Domain.Interfaces
{
    public interface IClientRepository
    {

        Task<PagedList<Client>> FindAll(int pageNumber, int pageSize);
        Task<PagedList<Client>> FindByFilter(string cpf, string name,
            string city, string neighbourhood, string phoneNumber, string fixPhoneNumber,
            int pageNumber, int pageSize);

        Task<PagedList<Client>> FindByFilter(string term, int pageNumber, int pageSize);
        Task<Client> FindById(int id);
        Task<Client> Insert(Client client);
        Task<Client> Update(Client client);
        Task<Client> Delete(int id);
        Task<bool> SaveAllAsync();

    }
}

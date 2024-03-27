using BookLoan.Domain.Entities;

namespace BookLoan.Domain.Interfaces
{
    public interface IClientRepository
    {

        Task<IEnumerable<Client>> FindAll();
        Task<Client> FindById(int id);
        Task<Client> Insert(Client client);
        Task<Client> Update(Client client);
        Task<Client> Delete(int id);
        Task<bool> SaveAllAsync();

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;

namespace BookLoan.Application.Services
{
    public class ClientService : IClientService
    {

        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ClientDTO>> FindAll()
        {
            IEnumerable<Client> clients = await _clientRepository.FindAll();
            IEnumerable<ClientDTO> clientDtos = _mapper.Map<IEnumerable<ClientDTO>>(clients);
            return clientDtos;

        }

        public async Task<ClientDTO> FindById(int id)
        {
            Client client = await _clientRepository.FindById(id);
            ClientDTO clientDto = _mapper.Map<ClientDTO>(client);
            return clientDto;
        }

        public async Task<ClientDTO> Insert(ClientDTO clientDTO)
        {

            Client client = _mapper.Map<Client>(clientDTO);
            Client clientInserted = await _clientRepository.Insert(client);
            return _mapper.Map<ClientDTO>(clientInserted);

        }

        public async Task<ClientDTO> Update(ClientDTO clientDTO)
        {
            Client client = _mapper.Map<Client>(clientDTO);
            Client clientChanged = await _clientRepository.Update(client);
            return _mapper.Map<ClientDTO>(clientChanged);
        }

        public async Task<ClientDTO> Delete(int id)
        {
            Client clientDeleted = await _clientRepository.Delete(id);
            return _mapper.Map<ClientDTO>(clientDeleted);
        }
    }
}

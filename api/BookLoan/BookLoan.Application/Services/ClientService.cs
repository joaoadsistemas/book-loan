﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Domain.Entities;
using BookLoan.Domain.Interfaces;
using BookLoan.Domain.Pagination;

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

        public async Task<PagedList<ClientDTO>> FindAll(int pageNumber, int pageSize)
        {
            var clients = await _clientRepository.FindAll(pageNumber, pageSize);
            var clientDtos = _mapper.Map<IEnumerable<ClientDTO>>(clients);
            return new PagedList<ClientDTO>(clientDtos, pageNumber, pageSize, clients.TotalCount);

        }

        public async Task<PagedList<ClientDTO>> FindByFilter(string cpf, string name, string city, string neighbourhood, string phoneNumber, string fixPhoneNumber,
            int pageNumber, int pageSize)
        {
            var clients = await _clientRepository.FindByFilter(cpf, name, city, neighbourhood, phoneNumber, fixPhoneNumber, pageNumber, pageSize);

            var clientsDTO = _mapper.Map<IEnumerable<ClientDTO>>(clients);

            return new PagedList<ClientDTO>(clientsDTO, pageNumber, pageSize, clients.TotalCount);
        }

        public async Task<PagedList<ClientDTO>> FindByFilter(string term, int pageNumber, int pageSize)
        {

            var clients = await _clientRepository.FindByFilter(term, pageNumber, pageSize);
            var clientsDTO = _mapper.Map<IEnumerable<ClientDTO>>(clients);

            return new PagedList<ClientDTO>(clientsDTO, pageNumber, pageSize, clients.TotalCount);

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

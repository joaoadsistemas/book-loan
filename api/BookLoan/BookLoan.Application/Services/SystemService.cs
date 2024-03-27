using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BookLoan.Application.DTOs;
using BookLoan.Application.Interfaces;
using BookLoan.Domain.Interfaces;

namespace BookLoan.Application.Services
{
    public class SystemService : ISystemService
    {
        private readonly ISystemRepository _systemRepository;
        private readonly IMapper _mapper;

        public SystemService(ISystemRepository systemRepository, IMapper mapper)
        {
            _systemRepository = systemRepository;
            _mapper = mapper;
        }

        public async Task<AmountItemDTO> SelectItemAmount()
        {
            var itemAmount = await _systemRepository.SelectItemAmount();
            var itemAmountDTO = _mapper.Map<AmountItemDTO>(itemAmount);
            return itemAmountDTO;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Application.DTOs;

namespace BookLoan.Application.Interfaces
{
    public interface ISystemService
    {
        Task<AmountItemDTO> SelectItemAmount();
    }
}

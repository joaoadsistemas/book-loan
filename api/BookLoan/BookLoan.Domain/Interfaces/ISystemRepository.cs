using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookLoan.Domain.SystemModels;

namespace BookLoan.Domain.Interfaces
{
    public interface ISystemRepository
    {
        Task<AmountItem> SelectItemAmount();
    }
}

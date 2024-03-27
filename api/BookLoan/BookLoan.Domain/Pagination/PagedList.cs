using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookLoan.Domain.Pagination
{
    public class PagedList<T> : List<T>
    {

        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }


        public PagedList()
        {
            
        }

        public PagedList(IEnumerable<T> items, int pageNumber, int pageSize, int count )
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSize);
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }

        public PagedList(IEnumerable<T> items, int totalPages, int pageNumber, int pageSize, int count)
        {
            CurrentPage = pageNumber;
            TotalPages = totalPages;
            PageSize = pageSize;
            TotalCount = count;
            AddRange(items);
        }



    }
}

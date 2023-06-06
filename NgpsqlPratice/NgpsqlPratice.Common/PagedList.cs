using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgpsqlPratice.Common
{
    public class PagedList<T> : List<T>
    {
        public int CurrentPage { get; set; }

        public int TotalPages { get; set; }

        public int PageSize { get; set; }

        public int TotalCount { get; set; }

        public PagedList(IEnumerable<T> currentPage, int count, int pageNumber, int pageSzie)
        {
            CurrentPage = pageNumber;
            TotalPages = (int) Math.Ceiling(count / (double) pageSzie);
            PageSize = pageSzie;
            TotalCount = count;
            AddRange(currentPage);
        }
    }
}

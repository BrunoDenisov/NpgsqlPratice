using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NgpsqlPratice.WebApi.Models
{
    public class Paging
    {
        public int PageSize {  get; set; }

        public int? PageNumber { get; set; }
        
        public int TotalCount { get; set; }

        public int TotalPages => (int)Math.Ceiling((double)TotalCount / PageSize);
    }
}
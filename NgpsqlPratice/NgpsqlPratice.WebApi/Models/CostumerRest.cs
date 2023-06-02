using NgpsqlPratice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NgpsqlPratice.WebApi.Models
{
    public class CostumerRest
    {
        public Guid mapId { get; set; }
        public List<Costumer> mappedList { get; set; }
    }
}
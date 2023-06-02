using NgpsqlPratice.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NgpsqlPratice.WebApi.Models
{
    public class CostumerRest
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }
    }
}
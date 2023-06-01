using System;
using NgpsqlPratice.Model.Common;

namespace NgpsqlPratice.Model
{
    public class Costumer:ICostumer
    {
        public Guid Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

        public int PhoneNumber { get; set; }
    }
}

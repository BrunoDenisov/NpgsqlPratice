using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NgpsqlPratice.Model.Common
{
    public interface ICostumer
    {
        Guid Id { get; set; }

        string FirstName { get; set; }

        string LastName { get; set; }

        string Gender { get; set; }

        string Email { get; set; }

        int PhoneNumber { get; set; }
    }
}

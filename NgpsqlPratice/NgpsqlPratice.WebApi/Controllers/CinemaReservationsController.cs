using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NgpsqlPratice.WebApi.Controllers
{
    public class CinemaReservationsController : ApiController
    {
        // GET: api/CinemaReservations
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/CinemaReservations/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/CinemaReservations
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/CinemaReservations/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/CinemaReservations/5
        public void Delete(int id)
        {
        }
    }
}

using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace NgpsqlPratice.WebApi.Controllers
{
    public class CinemaReservationsController : ApiController
    {
        static string connString = "Server=localhost;Port=5432;User Id=postgres;Password=12345678;Database=CinemaReservations";

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
        public HttpResponseMessage Post()
        {
            Guid costumerGuid;
            Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(connString);
            using (conn)
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "insert into costumer (id, first_name, last_name, gender, emial, phonenumber) values ('f504a1c2-2412-4ece-a81d-42039d228941'::UUID,'Jhon','Doe','M','jhon.doemail.com',553631421);";
                int noRowsAffected = cmd.ExecuteNonQuery();
                if(noRowsAffected > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK);
                }
            }
            return Request.CreateResponse(HttpStatusCode.BadRequest);
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

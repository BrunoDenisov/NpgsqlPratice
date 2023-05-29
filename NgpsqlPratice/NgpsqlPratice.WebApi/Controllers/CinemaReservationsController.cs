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
    public class Costumer
    {
        public Guid Id { get; set; }

        public string First_Name { get; set; }

        public string Last_Name { get; set; }

        public string Gender { get; set; }

        public string Emial { get; set; }

        public int PhoneNumber { get; set; }

        public Guid GuidGnerate()
        {
            Id = Guid.NewGuid();
            return Id;
        }
    }

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
        public HttpResponseMessage Post([FromBody] Costumer costumer)
        {
            Npgsql.NpgsqlConnection conn = new Npgsql.NpgsqlConnection(connString);
            using (conn)
            {
                conn.Open();
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = $"insert into costumer (Id, first_name, last_name, gender, emial, phonenumber) values (@Id,@first_name,@last_name,@gender,@emial,@phonenumber);";
                cmd.Parameters.AddWithValue("Id", costumer.GuidGnerate());
                cmd.Parameters.AddWithValue("first_name", costumer.First_Name);
                cmd.Parameters.AddWithValue("last_name", costumer.Last_Name);
                cmd.Parameters.AddWithValue("gender", costumer.Gender);
                cmd.Parameters.AddWithValue("emial", costumer.Emial);
                cmd.Parameters.AddWithValue("phonenumber", costumer.PhoneNumber);
                int noRowsAffected = cmd.ExecuteNonQuery();
                if(noRowsAffected > 0)
                {
                    return Request.CreateResponse(HttpStatusCode.OK,"Row inserted");
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

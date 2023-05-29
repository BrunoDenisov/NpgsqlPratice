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

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Gender { get; set; }

        public string Email { get; set; }

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
        public HttpResponseMessage Get()
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                using (conn)
                {
                    Costumer costumer = new Costumer();
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection= conn;
                    cmd.CommandText = $"select * from costumer;";
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    List<Costumer> list = new List<Costumer> { costumer };
                    while (reader.Read())
                    {
                        //list.Add();
                    }
                    return Request.CreateResponse(HttpStatusCode.OK, list);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        // POST: api/CinemaReservations
        public HttpResponseMessage Post([FromBody] Costumer costumer)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            costumer.Id = Guid.NewGuid();
            try
            {
                using (conn)
                {
                    conn.Open();
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"insert into costumer (\"Id\", \"FirstName\",\"LastName\", \"Gender\", \"Email\", \"PhoneNumber\") values (@id,@first_name,@last_name,@gender,@email,@phonenumber);";
                    cmd.Parameters.AddWithValue("@id", costumer.Id);
                    cmd.Parameters.AddWithValue("@first_name", costumer.FirstName);
                    cmd.Parameters.AddWithValue("@last_name", costumer.LastName);
                    cmd.Parameters.AddWithValue("@gender", costumer.Gender);
                    cmd.Parameters.AddWithValue("@email", costumer.Email);
                    cmd.Parameters.AddWithValue("@phonenumber", costumer.PhoneNumber);
                    int noRowsAffected = cmd.ExecuteNonQuery();
                    if (noRowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Row inserted");
                    }
                }
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}

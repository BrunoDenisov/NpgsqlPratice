using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Policy;
using System.Text;
using System.Web;
using System.Web.Http;
using System.Web.UI.WebControls;

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

        /*public Guid GuidGnerate()
        {
            Id = Guid.NewGuid();
            return Id;
        }*/
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
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection= conn;
                    cmd.CommandText = $"select * from \"Costumer\";";
                    conn.Open();
                    NpgsqlDataReader reader = cmd.ExecuteReader();
                    List<Costumer> list = new List<Costumer> ();
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            list.Add(new Costumer()
                            {
                                Id = (Guid)reader["Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber  = (int)reader["PhoneNumber"]
                            });
                        }
                        return Request.CreateResponse(HttpStatusCode.OK, list);
                    }
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No rows found");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
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
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"insert into \"Costumer\" (\"Id\", \"FirstName\",\"LastName\", \"Gender\", \"Email\", \"PhoneNumber\") values (@id,@first_name,@last_name,@gender,@email,@phonenumber);";
                    conn.Open();
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

        //DELTE: api/CinemaReservations
        public HttpResponseMessage Delete(Guid Id)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            Costumer getCostumer = GetCostumerByID(Id);

            if (getCostumer == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Costumer dosen't exist");
            }
            try
            {
                using (conn)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"delete from  \"Costumer\" where \"Id\"=@Id;";
                    conn.Open();
                    cmd.Parameters.AddWithValue("Id", getCostumer.Id);
                    int noRowsAffected = cmd.ExecuteNonQuery();
                    if (noRowsAffected > 0)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Rows delted");
                    }
                    return Request.CreateResponse(HttpStatusCode.NotFound, "No rows do delte");
                }
            }
            catch (Exception ex)
            {

                return Request.CreateResponse(HttpStatusCode.BadRequest, ex);
            }
        }


        // PUT: api/CinemaReservations/put
        public HttpResponseMessage Put(Guid Id, [FromBody] Costumer costumer)
        {
            NpgsqlConnection conn = new NpgsqlConnection (connString);

            Costumer getCostumer = GetCostumerByID(Id);

            if(getCostumer == null)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Costumer dosen't exist");
            }
            try
            {
                using (conn)
                {
                    var queryBuilder = new StringBuilder("");
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    queryBuilder.Append($"update \"Costumer\" Set");

                    cmd.Connection = conn;
                    conn.Open ();

                    if(costumer.FirstName == null || costumer.LastName == "")
                    {
                        cmd.Parameters.AddWithValue("@FirstName", costumer.FirstName = getCostumer.FirstName);
                    }
                    queryBuilder.Append($"\"FirstName\" = @FirstName,");
                    cmd.Parameters.AddWithValue("@Firstname", costumer.FirstName);
                    if (costumer.LastName == null || costumer.LastName == "")
                    {
                        cmd.Parameters.AddWithValue("@LastName", costumer.LastName = getCostumer.LastName);
                    }
                    queryBuilder.Append("\"LastName\" = @LastName,");
                    cmd.Parameters.AddWithValue("@LastName", costumer.LastName);
                    if (costumer.Gender == null || costumer.Gender == "")
                    {
                        cmd.Parameters.AddWithValue("@Gender", costumer.Gender = getCostumer.Gender);
                    }
                    queryBuilder.Append("\"Gender\" = @Gender,");
                    cmd.Parameters.AddWithValue("@Gender", costumer.Gender);
                    if (costumer.Email == null || costumer.Email == "")
                    {
                        cmd.Parameters.AddWithValue("@Email", costumer.Email = getCostumer.Email);
                    }
                    queryBuilder.Append("\"Email\" = @Email,");
                    cmd.Parameters.AddWithValue("@Email", costumer.Email);

                    if (queryBuilder.ToString().EndsWith(","))
                    {
                        if(queryBuilder.Length > 0)
                        {
                            queryBuilder.Remove(queryBuilder.Length - 1, 1);
                        }
                    }

                    queryBuilder.Append(" WHERE \"Id\"=@Id");
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.CommandText = queryBuilder.ToString();
                    cmd.ExecuteNonQuery();
                    return Request.CreateResponse(HttpStatusCode.OK, "Costumer updated sucesfully");
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        private Costumer GetCostumerByID(Guid Id)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            using (conn)
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = $"SELECT * from \"Costumer\" where \"Id\"=@id;";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", Id);
                conn.Open();
                NpgsqlDataReader reader = cmd.ExecuteReader();
                if(reader.HasRows)
                {
                    reader.Read();
                    Costumer costumer = new Costumer();
                    costumer.Id = Id;
                    costumer.FirstName = (string)reader["FirstName"];
                    costumer.LastName = (string)reader["LastName"];
                    costumer.Gender = (string)reader["Gender"];
                    costumer.Email = (string)reader["Email"];
                    costumer.PhoneNumber = (int)reader["PhoneNumber"];
                    return costumer;
                }
                return null;
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using NgpsqlPratice.Model.Common;
using NgpsqlPratice.Model;
using NgpsqlPratice.Repository.Common;
using Npgsql;
using System.Runtime.InteropServices;
using NgpsqlPratice.WebApi.Models;
using NgpsqlPratice.Common;

namespace NgpsqlPratice.Repository
{
    public class CostumerRepository
    {
        static string connString = "Server=localhost;Port=5432;User Id=postgres;Password=12345678;Database=CinemaReservations";

        public async Task<List<Costumer>>  Get()
        {

            NpgsqlConnection conn = new NpgsqlConnection(connString);
            try
            {
                using (conn)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = $"select * from \"Costumer\";";
                    conn.Open();
                    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                    List<Costumer> list = new List<Costumer>();
                    if (reader.HasRows)
                    {
                        while (await reader.ReadAsync())
                        {
                            list.Add(new Costumer()
                            {
                                Id = (Guid)reader["Id"],
                                FirstName = reader["FirstName"].ToString(),
                                LastName = reader["LastName"].ToString(),
                                Gender = reader["Gender"].ToString(),
                                Email = reader["Email"].ToString(),
                                PhoneNumber = (int)reader["PhoneNumber"]
                            });
                        }
                        return list;
                    }
                    return null;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // POST: api/CinemaReservations
        public async Task<int> Post(Costumer costumer)
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
                    int noRowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (noRowsAffected > 0)
                    {
                        return 1;
                    }
                }
                return 2;
            }
            catch (Exception ex)
            {

                return 3;
            }
        }

        //DELTE: api/CinemaReservations
        public async Task<int> Delete(Guid Id)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            Costumer getCostumer = await GetCostumerByID(Id);

            if (getCostumer == null)
            {
                return 1;
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
                    int noRowsAffected = await cmd.ExecuteNonQueryAsync();
                    if (noRowsAffected > 0)
                    {
                        return 2;
                    }
                    return 4;
                }
            }
            catch (Exception ex)
            {

                return 3;
            }
        }


        // PUT: api/CinemaReservations/put
        public async Task<int> Put(Guid Id, Costumer costumer)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);

            Costumer getCostumer = await GetCostumerByID(Id);

            if (getCostumer == null)
            {
                return 1;
            }
            try
            {
                using (conn)
                {
                    var queryBuilder = new StringBuilder("");
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    queryBuilder.Append($"update \"Costumer\" Set");

                    cmd.Connection = conn;
                    conn.Open();

                    if (costumer.FirstName == null || costumer.LastName == "") // string.isNullOrEmpty metoda bolja za koristiti (na svim if)
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
                        if (queryBuilder.Length > 0)
                        {
                            queryBuilder.Remove(queryBuilder.Length - 1, 1);
                        }
                    }

                    queryBuilder.Append(" WHERE \"Id\"=@Id");
                    cmd.Parameters.AddWithValue("@Id", Id);
                    cmd.CommandText = queryBuilder.ToString();
                    await cmd.ExecuteNonQueryAsync();
                    return 2;
                }
            }
            catch (Exception ex)
            {
                return 3;
            }
        }

        public async Task<Costumer> GetCostumerByID(Guid Id)
        {
            NpgsqlConnection conn = new NpgsqlConnection(connString);
            using (conn)
            {
                NpgsqlCommand cmd = new NpgsqlCommand();
                cmd.CommandText = $"SELECT * from \"Costumer\" where \"Id\"=@id;";
                cmd.Connection = conn;
                cmd.Parameters.AddWithValue("id", Id);
                conn.Open();
                NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();
                if (reader.HasRows)
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

        public async Task<PagedList<Costumer>> GetAll(Filtering filtering, Paging paging, Sorting sorting)
        {
            // Make filtering, paging, sorting in serive nad repository as well
            // make paged list class that will track how many items were returend on what page the I am on ect.
            NpgsqlConnection connection = new NpgsqlConnection(connString);
            try
            {
              using(connection)
                {
                    NpgsqlCommand cmd = new NpgsqlCommand();
                    var queryBuilder = new StringBuilder();

                    cmd.Connection = connection;
                    connection.Open();

                    queryBuilder.Append($"select * from \"Costumer\"");

                    if( filtering != null )
                    {
                        queryBuilder.Append($" where 1=1");
                        if (filtering.SearchQuery != null)
                        {
                            queryBuilder.Append($" and \"FirstName\" like @search");
                            cmd.Parameters.AddWithValue("@search", "%" + filtering.SearchQuery + "%");
                        }
                        if (filtering.FilterByGender != null)
                        {
                            queryBuilder.Append($" and \"Gender\" = @filterByGender");
                            cmd.Parameters.AddWithValue("@filterByGender", filtering.FilterByGender);
                        }
                        if (sorting.SortByLastName != null || false)
                        {
                            queryBuilder.Append($" order by \"LastName\"");
                            cmd.Parameters.AddWithValue("sortByLastName", sorting.SortByLastName);
                            
                        }
                        if (paging.PageSize != 1)
                        {
                            queryBuilder.Append($" limit @pageSize");
                            cmd.Parameters.AddWithValue("@pageSize", paging.PageSize);  
                        }
                        if (paging.PageNumber != 1)
                        {
                            queryBuilder.Append($" offset @pageNumber");
                            cmd.Parameters.AddWithValue("pageNumber", paging.PageSize * ((paging.PageNumber ?? 1) - 1));
                        }
                    }
                    

                    cmd.CommandText = queryBuilder.ToString();
                    await cmd.ExecuteNonQueryAsync();

                    NpgsqlDataReader reader = await cmd.ExecuteReaderAsync();

                    List<Costumer> list = new List<Costumer>();

                    while (await reader.ReadAsync())
                    {
                        list.Add(new Costumer()
                        {
                            Id =  (Guid)reader["Id"],
                            FirstName = reader["FirstName"].ToString(),
                            LastName = reader["LastName"].ToString(),
                            Gender = reader["Gender"].ToString(),
                            Email = reader["Email"].ToString(),
                            PhoneNumber = (int)reader["PhoneNumber"]
                        });
                    }

                    reader.Close();

                    NpgsqlCommand cmdCount = new NpgsqlCommand();
                    StringBuilder countQuery = new StringBuilder();
                    countQuery.Append($"Select count(*) from \"Costumer\"");

                    if(filtering != null)
                    {
                        countQuery.Append($" where 1=1");
                        if(filtering.SearchQuery != null)
                        {
                            countQuery.Append($" and \"FirstName\" like @search");
                            cmdCount.Parameters.AddWithValue("@search", "%" + filtering.SearchQuery + "%");
                        }
                        if (filtering.FilterByGender != null)
                        {
                            countQuery.Append($" and \"Gender\" = @filterByGender");
                            cmdCount.Parameters.AddWithValue("@filterByGender", filtering.FilterByGender);
                        }
                        if (sorting.SortByLastName != null || false)
                        {
                            countQuery.Append($" order by \"LastName\"");
                            cmdCount.Parameters.AddWithValue("sortByLastName", sorting.SortByLastName);
                        }
                        if (paging.PageSize != 1)
                        {
                            countQuery.Append($" limit @pageSize");
                            cmdCount.Parameters.AddWithValue("@pageSize", paging.PageSize);
                        }
                        if (paging.PageNumber != 1)
                        {
                            countQuery.Append($" offset @pageNumber");
                            cmdCount.Parameters.AddWithValue("pageNumber", paging.PageSize * ((paging.PageNumber ?? 1) - 1));
                        }
                    }

                    cmdCount.CommandText = countQuery.ToString();

                    paging.TotalCount = Convert.ToInt32(await cmd.ExecuteScalarAsync());

                    PagedList<Costumer> pagedCostumers = new PagedList<Costumer>(list, paging.TotalCount, paging.PageNumber ?? 1, paging.PageSize);
                    return pagedCostumers;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

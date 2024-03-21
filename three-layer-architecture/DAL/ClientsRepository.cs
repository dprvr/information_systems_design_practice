using Entities;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;


namespace DAL
{
    public class ClientsRepository : IRepository<Client>
    {

        private readonly string DbConnectinonString = @"Data Source=GST-PK\SQLEXPRESS;Initial Catalog=Sales_2;Integrated Security=True"; 
       
        public void Create(Client item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnectinonString))
            {
                string SqlProcedureName = "Create";
                SqlCommand sqlCommand = new SqlCommand(SqlProcedureName, sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter {ParameterName = "@FirstName", Value = item.FirstName },
                        new SqlParameter {ParameterName = "@LastName", Value = item.LastName },
                        new SqlParameter {ParameterName = "@Address", Value = item.Address },
                        new SqlParameter {ParameterName = "@Phone", Value = item.Phone },
                        new SqlParameter {ParameterName = "@City", Value = item.City }
                    };
                sqlCommand.Parameters.AddRange(sqlParameters);
                
                sqlConnection.Open();               
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public void Delete(int id)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnectinonString))
            {
                string SqlProcedureName = "Delete";
                SqlCommand sqlCommand = new SqlCommand(SqlProcedureName, sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                sqlCommand.Parameters.AddWithValue("@selectID", id);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }

        public Client Get(int id)
        {
            Client Client = null;
            using (SqlConnection sqlConnection = new SqlConnection(DbConnectinonString))
            {
                string SqlProcedureName = "Get";
                SqlCommand sqlCommand = new SqlCommand(SqlProcedureName, sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@selectID", id);

                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Client = new Client((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3],
                        (string)reader[4], (string)reader[5]);
                }
                reader.Close();
                sqlConnection.Close();
            }
            return Client;
        }

        public IEnumerable<Client> GetAll()
        {
            List<Client> Clients = new List<Client>();
            using (SqlConnection sqlConnection = new SqlConnection(DbConnectinonString))
            {
                string SqlProcedureName = "GetAll";                   
                SqlCommand sqlCommand = new SqlCommand(SqlProcedureName, sqlConnection);
                sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                
                sqlConnection.Open();
                SqlDataReader reader = sqlCommand.ExecuteReader();
                while (reader.Read())
                {
                    Clients.Add(new Client((int)reader[0], (string)reader[1], (string)reader[2], (string)reader[3],
                        (string)reader[4], (string)reader[5]));                    
                }
                reader.Close();
                sqlConnection.Close();
            }
            return Clients;
        }

        public void Update(Client item)
        {
            using (SqlConnection sqlConnection = new SqlConnection(DbConnectinonString))
            {
                string SqlProcedureName = "Update";
                SqlCommand sqlCommand = new SqlCommand(SqlProcedureName, sqlConnection)
                {
                    CommandType = System.Data.CommandType.StoredProcedure
                };
                SqlParameter[] sqlParameters = new SqlParameter[]
                    {
                        new SqlParameter {ParameterName = "@selectID", Value = item.ID },
                        new SqlParameter {ParameterName = "@FirstName", Value = item.FirstName },
                        new SqlParameter {ParameterName = "@LastName", Value = item.LastName },
                        new SqlParameter {ParameterName = "@Address", Value = item.Address },
                        new SqlParameter {ParameterName = "@Phone", Value = item.Phone },
                        new SqlParameter {ParameterName = "@City", Value = item.City }
                    };
                sqlCommand.Parameters.AddRange(sqlParameters);

                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                sqlConnection.Close();
            }
        }
    }
}

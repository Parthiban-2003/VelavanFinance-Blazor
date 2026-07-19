using Microsoft.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace VelavanFinanceERP.Services
{
    public class DatabaseService
    {
        private readonly string _connectionString;

        // Constructor - appsettings.json connection string taken
        public DatabaseService(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        // 1. Connection Open Helper Method
        private SqlConnection GetConnection()
        {
            return new SqlConnection(_connectionString);
        }

        // 2. SELECT Queries (DataTable return  -  DataGridView helpfull)
        public DataTable ExecuteQuery(string query, Dictionary<string, object> parameters = null)
        {
            DataTable dt = new DataTable();
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    // SQL Injection prevent parameters added
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    conn.Open();
                    using (SqlDataAdapter adapter = new SqlDataAdapter(cmd))
                    {
                        adapter.Fill(dt);
                    }
                }
            }
            return dt;
        }

        // 3. INSERT, UPDATE, DELETE Queries (Rows affected count return)
        public int ExecuteNonQuery(string query, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    conn.Open();
                    return cmd.ExecuteNonQuery();
                }
            }
        }

        // 4. Single Value Return (Example: MAX(LOAN_NO) or COUNT(*))
        public object ExecuteScalar(string query, Dictionary<string, object> parameters = null)
        {
            using (SqlConnection conn = GetConnection())
            {
                using (SqlCommand cmd = new SqlCommand(query, conn))
                {
                    if (parameters != null)
                    {
                        foreach (var param in parameters)
                        {
                            cmd.Parameters.AddWithValue(param.Key, param.Value ?? DBNull.Value);
                        }
                    }

                    conn.Open();
                    return cmd.ExecuteScalar();
                }
            }
        }
    }
}
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostgreWpf
{
    class DBDriver
    {
        NpgsqlConnection dbConnection;
        private DataSet ds = new DataSet();
        private DataTable dt = new DataTable();
        private String connectionString;

        public DBDriver(String server, String port, String userId, String password, String db)
        {
            try
            {
                connectionString = String.Format("Server={0};Port={1};" +
                    "User Id={2}; Password={3};Database={4}",
                    server, port, userId, password, db);
                dbConnection = new NpgsqlConnection(connectionString);
                dbConnection.Open();
            }
            catch (Exception msg)
            {
                Console.WriteLine(msg);
                throw;
            }
        }

        public DataTable GetDataTable()
        {
            return this.dt;
        }

        public DataTable ExecuteQueryAndReturn(String query)
        {
            try
            {
                NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, dbConnection);
                ds.Reset();
                da.Fill(ds);
                return ds.Tables[0];
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }

            return null;
        }

        public void ExecuteQuery(String query)
        {
            NpgsqlDataAdapter da = new NpgsqlDataAdapter(query, dbConnection);
            try
            {
                da.Fill(ds);
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void CloseConnection()
        {
            dbConnection.Close();
        }
    }
}

using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Data.SqlClient;

namespace ConsoleApp4.AdoDotNetExamples
{
    public class AdoDotNetExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "LAPTOP-73Q5S0H6",
            InitialCatalog = "Testdb",
            UserID = "sa",
            Password = "p@ssw0rd",
            TrustServerCertificate = true
        };

        public void Read()
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();

            SqlCommand cmd = new SqlCommand("Select * from Tbl_Blog", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();

            foreach (DataRow dr in dt.Rows)
            {
                Console.WriteLine(dr["BlogId"]);
                Console.WriteLine(dr["BlogTitle"]);
                Console.WriteLine(dr["BlogAuthor"]);
                Console.WriteLine(dr["BlogContent"]);
            }


        }
        public void Write(string id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);

            connection.Open();
            SqlCommand cmd = new SqlCommand($"Select * from Tbl_Blog where BlogId = '{id}'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            adapter.Fill(dt);
            connection.Close();

            if (dt.Rows.Count == 0)
            {
                Console.WriteLine("No Data Found");
                return;
            }
            DataRow dr = dt.Rows[0];

            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitle"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);
        }
        public void Create(string BlogTitle, string BlogAuthor, string BlogContent)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('
            {BlogTitle}'
           ,'{BlogAuthor}'
           ,'{BlogContent}')";
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand(query, connection);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Success Message" : "Failed Message";
            Console.WriteLine(message);

        }
        public void Update(string id, string BlogTitle)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand($"Update Tbl_Blog Set BlogId = '{id}' where BlogTitle = '{BlogTitle}'", connection);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            connection.Close();
            if (dt == null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            DataRow dr = dt.Rows[0];

            Console.WriteLine(dr["BlogId"]);
            Console.WriteLine(dr["BlogTitel"]);
            Console.WriteLine(dr["BlogAuthor"]);
            Console.WriteLine(dr["BlogContent"]);

        }
        public void Delete(string id)
        {
            SqlConnection connection = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            connection.Open();

            SqlCommand cmd = new SqlCommand($"Delete from Tbl_Blog where BlogId = '{id}'", connection);
            int result = cmd.ExecuteNonQuery();

            connection.Close();

            string message = result > 0 ? "Success Message" : "Failed Message";
            Console.WriteLine(message);
        }
    }
}

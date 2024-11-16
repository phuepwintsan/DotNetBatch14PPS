using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14PPS.ConsoleApp
{

    internal class Program
    {

        static void Main(string[] args)
        {
            SqlConnectionStringBuilder connectionStringBuilder = new SqlConnectionStringBuilder();
            connectionStringBuilder.DataSource = "LAPTOP-73Q5S0H6";
            connectionStringBuilder.InitialCatalog = "Testdb";
            connectionStringBuilder.UserID = "sa";
            connectionStringBuilder.Password = "p@ssw0rd";
            connectionStringBuilder.TrustServerCertificate = true;

            SqlConnection connection = new SqlConnection(connectionStringBuilder.ConnectionString);
            connection.Open();
            string query = "Select * from Tbl_Blog";
            SqlCommand cmd = new SqlCommand(query,connection);
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
            Console.ReadLine();

            Console.WriteLine("Enter User Name");
            string UserName = Console.ReadLine();

            Console.WriteLine("Hello ");
            Console.WriteLine($"Hello {UserName}");

            Console.ReadLine();
        }
    }
}

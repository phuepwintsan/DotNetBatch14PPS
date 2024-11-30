using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using DotNetBatch14PPS.ConsoleApp4.Dtos;

namespace DotNetBatch14PPS.ConsoleApp4.DapperExamples
{
    public class DapperExample
    {
        private readonly string _connectionString = AppSetting.SqlConnectionStringBuilder.ConnectionString;

        public void Read()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            List<BlogDto> ls = connection.Query<BlogDto>("Select * from Tbl_Blog").ToList();

            foreach (BlogDto blog in ls)
            {
                Console.WriteLine(blog.BlogId);
                Console.WriteLine(blog.BlogTitle);
                Console.WriteLine(blog.BlogAuthor);
                Console.WriteLine(blog.BlogContent);
            }
        }
        public void Write(string id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            var item = connection
            .Query<BlogDto>($"select * from tbl_blog where BlogId = '{id}'")
            .FirstOrDefault();

            if (item == null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }
        public void Create(string title, string author, string content)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           ('
            {title}'
           ,'{author}'
           ,'{content}')";

            using IDbConnection connection = new SqlConnection(_connectionString);

            int result = connection.Execute(query);

            string message = result > 0 ? "Success Message" : "Failed Message";
            Console.WriteLine(message);

        }
        public void Update(string id, string BlogTitle)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            var lst = connection.Query<BlogDto>($"Update Tbl_Blog Set BlogId = '{id}' where BlogTitle = '{BlogTitle}'").FirstOrDefault();

            if (lst == null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

            Console.WriteLine(lst.BlogId);
            Console.WriteLine(lst.BlogTitle);
            Console.WriteLine(lst.BlogAuthor);
            Console.WriteLine(lst.BlogContent);
        }
        public void Delete(string id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            var lst = connection.Query<BlogDto>($"Delete From Tbl_Blog where BlogId = '{id}'").FirstOrDefault();

            if (lst == null)
            {
                Console.WriteLine("No Data Found");
                return;
            }
        }

    }
}

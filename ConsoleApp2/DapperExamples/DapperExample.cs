using ConsoleApp2.Dtos;
using Dapper;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.DapperExamples
{
    public class DapperExample
    {
        private readonly string _connectionString = AppSetting.SqlConnectionStringBuilder.ConnectionString;
        public void Read()
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            List<BlogDto> lst = connection
          .Query<BlogDto>("select * from tbl_blog")
          .ToList();


            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
            }
        }
        public void Edit(string id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            var item = connection
             .Query<BlogDto>($"select * from tbl_blog where BlogId = '{id}'")
             .FirstOrDefault();

            if (item == null)
            {
                Console.WriteLine("No Data found");
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

            var item = connection.Query<BlogDto>($"Update Tbl_Blog Set BlogTitle = '{BlogTitle}' where BlogId = '{id}'").FirstOrDefault();

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
        public void Delete(string id)
        {
            using IDbConnection connection = new SqlConnection(_connectionString);

            var item = connection.Query<BlogDto>($"Delete from Tbl_Blog where BlogId = '{id}'").FirstOrDefault();

            if (item == null)
            {
                Console.WriteLine("No Data Found");
                return;
            }

        }
    }
}

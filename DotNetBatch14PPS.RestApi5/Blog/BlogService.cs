using DotNetBatch14PPS.RestApi5.Blog;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DotNetBatch14PPS.RestApi5.Blog
{
    public class BlogService : IBlogService
    {
        private readonly SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder()
        {
            DataSource = "LAPTOP-73Q5S0H6",
            InitialCatalog = "Testdb",
            UserID = "sa",
            Password = "p@ssw0rd",
            TrustServerCertificate = true
        };

        public List<BlogModel> GetBlogs()
        {
            string query = "select * from tbl_blog";

            SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);

            con.Close();

            List<BlogModel> lstblogs = new List<BlogModel>();
            foreach (DataRow row in dt.Rows)
            {
                BlogModel blogmodel = new BlogModel();
                blogmodel.BlogId = row["BlogId"].ToString()!;
                blogmodel.BlogTitle = row["BlogTitle"].ToString()!;
                blogmodel.BlogAuthor = row["BlogAuthor"].ToString()!;
                blogmodel.BlogContent = row["BlogContent"].ToString()!;

                lstblogs.Add(blogmodel);
            }
            return lstblogs;
        }

        public BlogModel GetBlog(string id)
        {
            string query = "select * from tbl_blog where BlogId=@blogId";
            SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@blogId", id);
            SqlDataAdapter dataAdapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            dataAdapter.Fill(dt);

            con.Close();

            if (dt.Rows.Count == 0) return null;

            BlogModel model = new BlogModel();
            DataRow row = dt.Rows[0];

            model.BlogId = row["BlogId"].ToString()!;
            model.BlogTitle = row["BlogTitle"].ToString()!;
            model.BlogAuthor = row["BlogAuthor"].ToString()!;
            model.BlogContent = row["BlogContent"].ToString()!;

            return model;
        }

        public BlogResponseMode CreateBlog(BlogModel requestModel)
        {
            string query = $@"INSERT INTO [dbo].[Tbl_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";
            SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);

            int result = cmd.ExecuteNonQuery();

            con.Close();

            BlogResponseMode model = new BlogResponseMode();
            string message = result > 0 ? "Create success" : "Create Fail";
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }

        public BlogResponseMode UpdateBlog(BlogModel requestModel)
        {
            var item = GetBlog(requestModel.BlogId);
            if (item is null)
            {
                new BlogResponseMode()
                {
                    IsSuccess = false,
                    Message = "No Data found."
                };
            }

            if (string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                requestModel.BlogTitle = item.BlogTitle;
            }
            if (string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                requestModel.BlogAuthor = item.BlogAuthor;
            }
            if (string.IsNullOrEmpty(requestModel.BlogContent))
            {
                requestModel.BlogContent = item.BlogContent;
            }
            string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

            SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand(query, con);
            cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);
            cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
            cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
            cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
            int result = cmd.ExecuteNonQuery();

            con.Close();

            BlogResponseMode model = new BlogResponseMode();
            string message = result > 0 ? "Update success" : "Update Faile";
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }

        public BlogResponseMode UpSertBlog(BlogModel requestModel)
        {
            BlogResponseMode model = new BlogResponseMode();
            var item = GetBlog(requestModel.BlogId);
            if (item is not null)
            {
                if (string.IsNullOrEmpty(requestModel.BlogTitle))
                {
                    requestModel.BlogTitle = item.BlogTitle;
                }
                if (string.IsNullOrEmpty(requestModel.BlogAuthor))
                {
                    requestModel.BlogAuthor = item.BlogAuthor;
                }
                if (string.IsNullOrEmpty(requestModel.BlogContent))
                {
                    requestModel.BlogContent = item.BlogContent;
                }

                string query = @"UPDATE [dbo].[Tbl_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE BlogId = @BlogId";

                SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
                con.Open();

                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@BlogId", requestModel.BlogId);
                cmd.Parameters.AddWithValue("@BlogTitle", requestModel.BlogTitle);
                cmd.Parameters.AddWithValue("@BlogAuthor", requestModel.BlogAuthor);
                cmd.Parameters.AddWithValue("@BlogContent", requestModel.BlogContent);
                int result = cmd.ExecuteNonQuery();

                con.Close();

                string message = result > 0 ? "Update success" : "Update Fail";
                model.IsSuccess = result > 0;
                model.Message = message;
            }
            else if (item is null)
            {
                model = CreateBlog(requestModel);
            }

            return model;
        }

        public BlogResponseMode DeleteBlog(string id)
        {
            SqlConnection con = new SqlConnection(sqlConnectionStringBuilder.ConnectionString);
            con.Open();

            SqlCommand cmd = new SqlCommand("delete from tbl_blog where BlogId = @Id", con);
            cmd.Parameters.AddWithValue("@Id", id);
            int result = cmd.ExecuteNonQuery();
            con.Close();

            string message = result > 0 ? "Delete Successful" : "Delete Fail";
            BlogResponseMode model = new BlogResponseMode();

            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }

    }
}


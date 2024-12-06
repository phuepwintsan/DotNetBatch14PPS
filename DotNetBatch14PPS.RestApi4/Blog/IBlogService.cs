using DotNetBatch14PPS.RestApi4.Blog;

namespace DotNetBatch14PPS.RestApi4.Blog
{
    public interface IBlogService
    {
        BlogResponseMode CreateBlog(BlogModel requestModel);
        BlogResponseMode DeleteBlog(string id);
        BlogModel GetBlog(string id);
        List<BlogModel> GetBlogs();
        BlogResponseMode UpdateBlog(BlogModel requestModel);
        BlogResponseMode UpSertBlog(BlogModel requestModel);
    }
}
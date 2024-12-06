namespace DotNetBatch14PPS.RestApi.Features.Blog
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
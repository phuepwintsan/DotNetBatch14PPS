using DotNetBatch14PPS.RestApi5.Blog;
using Microsoft.EntityFrameworkCore;

namespace DotNetBatch14PPS.RestApi5.Blog
{
    public class BlogEFCoreService : IBlogService
    {

        private readonly AppDbCotext _db;
        public BlogEFCoreService()
        {
            _db = new AppDbCotext();
        }

        public BlogResponseMode CreateBlog(BlogModel requestModel)
        {
            requestModel.BlogId = Guid.NewGuid().ToString();

            _db.Blogs.Add(requestModel);
            var result = _db.SaveChanges();

            string message = result > 0 ? "Create Successful." : "Create Failed.";
            BlogResponseMode model = new BlogResponseMode();
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }

        public BlogResponseMode DeleteBlog(string id)
        {
            var item = _db.Blogs.AsNoTracking().Where(x => x.BlogId == id).FirstOrDefault();
            if (item is null)
            {
                new BlogResponseMode()
                {
                    IsSuccess = false,
                    Message = "No data found"
                };
            }
            _db.Entry(item).State = EntityState.Deleted;
            int result = _db.SaveChanges();

            string message = result > 0 ? "Delete Successful." : "Delete Failed.";
            BlogResponseMode model = new BlogResponseMode();
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;
        }

        public BlogModel GetBlog(string id)
        {
            var item = _db.Blogs
                    .AsNoTracking()
                    .Where(x => x.BlogId == id).FirstOrDefault();
            return item!;
        }

        public List<BlogModel> GetBlogs()
        {
            var lst = _db.Blogs
                    .AsNoTracking()
                    .ToList();
            return lst;
        }

        public BlogResponseMode UpdateBlog(BlogModel requestModel)
        {
            var item = _db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == requestModel.BlogId);
            if (item is null)
            {
                return new BlogResponseMode
                {
                    IsSuccess = false,
                    Message = "No data found."
                };
            }

            if (!string.IsNullOrEmpty(requestModel.BlogTitle))
            {
                item.BlogTitle = requestModel.BlogTitle;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogAuthor))
            {
                item.BlogAuthor = requestModel.BlogAuthor;
            }
            if (!string.IsNullOrEmpty(requestModel.BlogContent))
            {
                item.BlogContent = requestModel.BlogContent;
            }

            _db.Entry(item).State = EntityState.Modified;
            var result = _db.SaveChanges();

            string message = result > 0 ? "Updating Successful" : "Updating Failed.";
            BlogResponseMode model = new BlogResponseMode();
            model.IsSuccess = result > 0;
            model.Message = message;

            return model;

        }

        public BlogResponseMode UpSertBlog(BlogModel requestModel)
        {
            BlogResponseMode model = new BlogResponseMode();
            var item = _db.Blogs.Where(x => x.BlogId == requestModel.BlogId).FirstOrDefault();
            if (item is not null)
            {
                if (string.IsNullOrEmpty(requestModel.BlogTitle))
                {
                    item.BlogTitle = requestModel.BlogTitle;
                }
                if (string.IsNullOrEmpty(requestModel.BlogAuthor))
                {
                    item.BlogAuthor = requestModel.BlogAuthor;
                }
                if (string.IsNullOrEmpty(requestModel.BlogContent))
                {
                    item.BlogContent = requestModel.BlogContent;
                }
                _db.Entry(item).State = EntityState.Modified;
                var result = _db.SaveChanges();

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
    }
}
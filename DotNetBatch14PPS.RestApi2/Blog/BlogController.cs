using DotNetBatch14PPS.RestApi.Features.Blog;
using Microsoft.AspNetCore.Mvc;

namespace DotNetBatch14PPS.RestApi2.Blog
{
    namespace DotNetBatch14PPS.RestApi.Features.Blog
    {
        [Route("api/[controller]")]
        [ApiController]
        public class BlogController : ControllerBase
        {
            private readonly IBlogService _blogService;

            public BlogController()
            {
                _blogService = new BlogEFCoreService();
            }

            [HttpGet]
            public IActionResult GetBlogs()
            {
                var model = _blogService.GetBlogs();
                return Ok(model);
            }

            [HttpGet("{id}")]
            public IActionResult GetBlog(string id)
            {
                var model = _blogService.GetBlog(id);
                if (model is null)
                {
                    return NotFound("No data found");
                }
                return Ok(model);
            }

            [HttpPost]
            public IActionResult CreateBlog([FromBody] BlogModel blogmodel)
            {
                var model = _blogService.CreateBlog(blogmodel);
                if (!model.IsSuccess)
                {
                    return BadRequest(model);
                }
                return Ok(model);
            }

            [HttpPatch("{id}")]
            public IActionResult PatchBlog(string id, BlogModel requestModel)
            {
                requestModel.BlogId = id;
                var model = _blogService.UpdateBlog(requestModel);
                if (!model.IsSuccess)
                {
                    return BadRequest(model);
                }
                return Ok(model);
            }

            [HttpPut("{id}")]
            public IActionResult UpSertBlog(string id, BlogModel requestModel)
            {
                requestModel.BlogId = id;
                var model = _blogService.UpSertBlog(requestModel);
                if (!model.IsSuccess)
                {
                    return BadRequest(model);
                }
                return Ok(model);
            }


            [HttpDelete("{id}")]
            public IActionResult Delete(string id)
            {
                var model = _blogService.DeleteBlog(id);
                if (!model.IsSuccess)
                {
                    return BadRequest(model);
                }
                return Ok(model);
            }
        }
    }
}

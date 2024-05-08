using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DKDotNetCore.RestApiWithNLayer.Features.Blog
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogController : ControllerBase
    {
        private readonly BL_Blog _blBlog;

        public BlogController()
        {
            _blBlog = new BL_Blog();
        }

        [HttpGet]
        public IActionResult Get()
        {
            var lst = _blBlog.GetBlogs();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult Edit(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item == null)
            {
                return NotFound("No data found");
            }

            return Ok(item);
        }

        [HttpPost]
        public IActionResult Create(BlogModel blog)
        {
            int result = _blBlog.CreateBlog(blog);

            String message = result > 0 ? "Saving successful!" : "Saving failed!";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);

            if (item == null)
            {
                return NotFound("No data found");
            }

            int result = _blBlog.UpdateBlog(id, blog);
            String message = result > 0 ? "Updating successful" : "Updating failed";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult Patch(int id, BlogModel blog)
        {
            var item = _blBlog.GetBlog(id);
            if (item == null)
            {
                return NotFound("No data found");
            }

            int result = _blBlog.PatchBlog(id, blog);
            String message = result > 0 ? "Updating successful" : "Updating failed";
            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var item = _blBlog.GetBlog(id);
            if (item == null)
            {
                return NotFound("No data found");
            }

            int result = _blBlog.DeleteBlog(id);

            String message = result > 0 ? "Deleting successful" : "Deleting failed";
            return Ok(message);
        }
    }
}

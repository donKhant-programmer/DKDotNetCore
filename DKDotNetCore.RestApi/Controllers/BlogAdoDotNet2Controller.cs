using DKDotNetCore.RestApi.Models;
using DKDotNetCore.Shared;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Reflection.Metadata;

namespace DKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogAdoDotNet2Controller : ControllerBase
    {
        private readonly AdoDotNetService _adoDotNetService = new AdoDotNetService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        [HttpGet]
        public IActionResult GetBlogs()
        {
            String query = "select * from table_blog";

            var lst = _adoDotNetService.Query<BlogModel>(query);

            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult GetBlog(int id)
        {

            String query = "select * from table_blog where BlogId = @BlogId";

            //AdoDotNetParameter[] parameters = new AdoDotNetParameter[1];
            //parameters[0] = new AdoDotNetParameter("@BlogId", id);
            //var lst = _adoDotNetService.Query<BlogModel>(query, parameters);

            var blog = _adoDotNetService.QueryFirstOrDefault<BlogModel>(query, new AdoDotNetParameter("@BlogId", id));

            if (blog == null)
            {
                return NotFound("No data found");
            }
            return Ok(blog);
        }

        [HttpPost]
        public IActionResult CreateBlog(BlogModel blog)
        {
            string query = @"INSERT INTO [dbo].[Table_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            int result = _adoDotNetService.Execute(
                query,
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "saving successful." : "saving failed.";

            return Ok(message);
            //return StatusCode(500, message);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateBlog(int id, BlogModel blog)
        {
            string query = @"UPDATE [dbo].[Table_Blog]
   SET [BlogTitle] = @BlogTitle
      ,[BlogAuthor] = @BlogAuthor
      ,[BlogContent] = @BlogContent
 WHERE [BlogId] = @BlogId ";

            blog.BlogId = id;

            int result = _adoDotNetService.Execute(
                query,
                new AdoDotNetParameter("@BlogId", id),
                new AdoDotNetParameter("@BlogTitle", blog.BlogTitle),
                new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor),
                new AdoDotNetParameter("@BlogContent", blog.BlogContent)
                );

            string message = result > 0 ? "Updating successful." : "Updating failed.";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult PatchBlog(int id, BlogModel blog)
        {
            List<AdoDotNetParameter> lst = new List<AdoDotNetParameter>();

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle, ";
                lst.Add(new AdoDotNetParameter("@BlogTitle", blog.BlogTitle));
            }
            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor, ";
                lst.Add(new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor));
            }
            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] = @BlogContent, ";
                lst.Add(new AdoDotNetParameter("@BlogContent", blog.BlogContent));
            }

            lst.Add(new AdoDotNetParameter("@BlogId", id));

            if (conditions.Length == 0)
            {
                var response = new { IsSuccess = false, Message = "No data found." };
                return NotFound(response);
            }

            conditions = conditions[..^2];


            string query = $@"UPDATE [dbo].[Table_Blog]
           SET {conditions} where [BlogId] = @BlogId";


            int result = _adoDotNetService.Execute(
                query, lst.ToArray()
            );

            string message = result > 0 ? "Updating successful." : "Updating failed.";

            return Ok(message);
        }

        //[HttpPatch("{id}")]
        //public IActionResult PatchBlog2(int id, BlogModel blog)
        //{
        //    int numOfParameters = 0;
        //    numOfParameters++; // for blogId

        //    string conditions = string.Empty;
        //    if (!string.IsNullOrEmpty(blog.BlogTitle))
        //    {
        //        conditions += "[BlogTitle] = @BlogTitle, ";
        //        numOfParameters++;
        //    }
        //    if (!string.IsNullOrEmpty(blog.BlogAuthor))
        //    {
        //        conditions += "[BlogAuthor] = @BlogAuthor, ";
        //        numOfParameters++;
        //    }
        //    if (!string.IsNullOrEmpty(blog.BlogContent))
        //    {
        //        conditions += "[BlogContent] = @BlogContent, ";
        //        numOfParameters++;
        //    }

        //    if (conditions.Length == 0)
        //    {
        //        var response = new { IsSuccess = false, Message = "No data found." };
        //        return NotFound(response);
        //    }

        //    conditions = conditions[..^2];


        //    int parametersIndex = 0;
        //    AdoDotNetParameter[] parameters = new AdoDotNetParameter[numOfParameters];
        //    parameters[parametersIndex] = new AdoDotNetParameter("@BlogId", id);
        //    parametersIndex++;

        //    if (!string.IsNullOrEmpty(blog.BlogTitle))
        //    {
        //        parameters[parametersIndex] = new AdoDotNetParameter("@BlogTitle", blog.BlogTitle);
        //        parametersIndex++;
        //    }

        //    if (!string.IsNullOrEmpty(blog.BlogAuthor))
        //    {
        //        parameters[parametersIndex] = new AdoDotNetParameter("@BlogAuthor", blog.BlogAuthor);
        //        parametersIndex++;
        //    }

        //    if (!string.IsNullOrEmpty(blog.BlogContent))
        //    {
        //        parameters[parametersIndex] = new AdoDotNetParameter("@BlogContent", blog.BlogContent);
        //    }


        //    string query = $@"UPDATE [dbo].[Table_Blog]
        //   SET {conditions} where [BlogId] = @BlogId";


        //    int result = _adoDotNetService.Execute(
        //        query, parameters
        //    );

        //    string message = result > 0 ? "Updating successful." : "Updating failed.";

        //    return Ok(message);
        //}

        [HttpDelete("{id}")]
        public IActionResult DeleteBlog(int id)
        {
            string query = @"DELETE FROM [dbo].[Table_Blog]
      WHERE [BlogId] = @BlogId";

            int result = _adoDotNetService.Execute(
                query,
                new AdoDotNetParameter("@BlogId", id)
            );

            string message = result > 0 ? "Deleting successful." : "Deleting failed.";

            return Ok(message);
        }

    }
}






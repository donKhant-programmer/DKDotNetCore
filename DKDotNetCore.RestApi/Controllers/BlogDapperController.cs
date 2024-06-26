﻿using Dapper;
using DKDotNetCore.RestApi.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DKDotNetCore.RestApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BlogDapperController : ControllerBase
    {
        [HttpGet]
        public IActionResult getBlogs()
        {
            string query = "select * from Table_Blog";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            List<BlogModel> lst = db.Query<BlogModel>(query).ToList();
            return Ok(lst);
        }

        [HttpGet("{id}")]
        public IActionResult getBlog(int id)
        {
            //string query = "select * from table_blog where blogId = @blogId";
            //using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            //var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();

            var item = findById(id);
            
            if (item is null)
            {
                return NotFound("No data found");
            }
            return Ok(item);
        }

        [HttpPost]
        public IActionResult createBlogs(BlogModel blog)
        {

            string query = @"INSERT INTO [dbo].[Table_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            String message = result > 0 ? "Saving successful!" : "Saving failed";

            return Ok(message);
        }

        [HttpPut("{id}")]
        public IActionResult updateBlog(int id, BlogModel blog)
        {
            var item = findById(id);

            if (item is null)
            {
                return NotFound("No data found");
            }
            blog.BlogId = id;
            string query = @"UPDATE [dbo].[Table_Blog]
           SET [BlogTitle] = @BlogTitle
           ,[BlogAuthor] = @BlogAuthor
           ,[BlogContent] = @BlogContent where [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            String message = result > 0 ? "Updating successful!" : "Updating failed!";

            return Ok(message);
        }

        [HttpPatch("{id}")]
        public IActionResult patchBlog(int id, BlogModel blog)
        {
            var item = findById(id);

            if (item is null)
            {
                return NotFound("No data found");
            }

            string conditions = string.Empty;
            if (!string.IsNullOrEmpty(blog.BlogTitle))
            {
                conditions += "[BlogTitle] = @BlogTitle, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogAuthor))
            {
                conditions += "[BlogAuthor] = @BlogAuthor, ";
            }

            if (!string.IsNullOrEmpty(blog.BlogContent))
            {
                conditions += "[BlogContent] = @BlogContent, ";
            }

            if (conditions.Length == 0)
            {
                return NotFound("No data to update");
            }

            //var temp = conditions.Substring(0, conditions.Length);
            conditions = conditions.Substring(0, conditions.Length - 2);

            blog.BlogId = id;

            string query = $@"UPDATE [dbo].[Table_Blog]
           SET {conditions} where [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, blog);

            String message = result > 0 ? "Updating successful!" : "Updating failed!";

            return Ok(message);
        }

        [HttpDelete("{id}")]
        public IActionResult deleteBlog(int id)
        {
            var item = findById(id);

            if (item is null)
            {
                return NotFound("No data found");
            }

            string query = @"DELETE [dbo].[Table_Blog] where [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, new BlogModel { BlogId = id});

            String message = result > 0 ? "Deleting successful!" : "Deleting failed!";

            return Ok(message);
        }

        private BlogModel? findById(int id)
        {
            string query = "select * from table_blog where blogId = @blogId";
            using IDbConnection db = new SqlConnection(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogModel>(query, new BlogModel { BlogId = id }).FirstOrDefault();
            return item;

        }
    }
}

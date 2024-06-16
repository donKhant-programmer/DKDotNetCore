using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKDotNetCore.ConsoleApp
{
    public class DapperExample
    {
        private readonly SqlConnectionStringBuilder _sqlConnectionStringBuilder;

        public DapperExample(SqlConnectionStringBuilder sqlConnectionStringBuilder)
        {
            _sqlConnectionStringBuilder = sqlConnectionStringBuilder;
        }

        public void Run() {
            //Read();
            //Edit(1);
            //Edit(20);
            //Create("intro to japanese", "Takashi", "xxxxxxxxxx");
            //Update(1, "sakura", "programmer", "guitarist");
            Delete(1);
            Read();
        }

        private void Read()
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            List<BlogDto> lst = db.Query<BlogDto>("select * from table_blog").ToList();

            foreach (BlogDto item in lst)
            {
                Console.WriteLine(item.BlogId);
                Console.WriteLine(item.BlogTitle);
                Console.WriteLine(item.BlogAuthor);
                Console.WriteLine(item.BlogContent);
                Console.WriteLine("-----------------------");
            }
        }

        private void Edit(int id)
        {
            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            var item = db.Query<BlogDto>("select * from table_blog where blogId = @blogId", new BlogDto { BlogId = id }).FirstOrDefault();
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }
            Console.WriteLine(item.BlogId);
            Console.WriteLine(item.BlogTitle);
            Console.WriteLine(item.BlogAuthor);
            Console.WriteLine(item.BlogContent);
        }

        private void Create(String title, String author, String content)
        {
            var item = new BlogDto
            {
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"INSERT INTO [dbo].[Table_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            String message = result > 0 ? "Saving successful!" : "Saving failed";
            Console.WriteLine(message);

        }

        private void Update(int id, String title, String author, String content)
        {
            var item = new BlogDto
            {
                BlogId = id,
                BlogTitle = title,
                BlogAuthor = author,
                BlogContent = content
            };

            string query = @"UPDATE [dbo].[Table_Blog]
           SET [BlogTitle] = @BlogTitle
           ,[BlogAuthor] = @BlogAuthor
           ,[BlogContent] = @BlogContent where [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            String message = result > 0 ? "Updating successful!" : "Updating failed!";
            Console.WriteLine(message);

        }

        private void Delete(int id)
        {
            var item = new BlogDto
            {
                BlogId = id,
            };

            string query = @"DELETE [dbo].[Table_Blog] where [BlogId] = @BlogId";

            using IDbConnection db = new SqlConnection(_sqlConnectionStringBuilder.ConnectionString);
            int result = db.Execute(query, item);

            String message = result > 0 ? "Deleting successful!" : "Deleting failed!";
            Console.WriteLine(message);

        }
    }
}

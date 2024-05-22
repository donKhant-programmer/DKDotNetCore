using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKDotNetCore.WinFormsApp.Queries
{
    internal class BlogQuery
    {
        public static string BlogCreate { get; } = @"INSERT INTO [dbo].[Table_Blog]
           ([BlogTitle]
           ,[BlogAuthor]
           ,[BlogContent])
     VALUES
           (@BlogTitle
           ,@BlogAuthor
           ,@BlogContent)";

        public static string Blogs { get; } = "select * from Table_Blog";

    }
}

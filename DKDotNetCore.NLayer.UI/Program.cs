// See https://aka.ms/new-console-template for more information
using DKDotNetCore.DataAccess.Features.Blog;
using System.Reflection.Metadata;

Console.WriteLine("Hello, World!");


BL_Blog bl_Blog = new BL_Blog();

var lst = bl_Blog.GetBlogs();
foreach (var item in lst)
{
    Console.WriteLine("Title: " + item.BlogTitle);
    Console.WriteLine("Author: " + item.BlogAuthor);
    Console.WriteLine("Content: " + item.BlogContent);
    Console.WriteLine("------------------------------");
}

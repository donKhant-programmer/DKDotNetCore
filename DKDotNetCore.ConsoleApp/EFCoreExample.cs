﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKDotNetCore.ConsoleApp
{
    internal class EFCoreExample
    {
        private readonly AppDbContext db = new AppDbContext();
        public void Run()
        {
            //Edit(2);
            //Edit(22);
            //Create("new T", "new author", "new content");
            //Update(4, "T", "A", "Content");
            Delete(2);
            Read();
        }

        private void Read()
        {
            var lst = db.Blogs.ToList();
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
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id); // similar to for each iteration
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
            
            db.Blogs.Add(item); // EFCore benefit -> auto assign id to the object we insert into db.
            int result = db.SaveChanges();

            String message = result > 0 ? "Saving successful!" : "Saving failed!";
            Console.WriteLine(message);
        }

        private void Update(int id, String title, String author, String content)
        {
            //var item = db.Blogs.AsNoTracking().FirstOrDefault(x => x.BlogId == id);
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id);
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            item.BlogTitle = title;
            item.BlogAuthor = author;
            item.BlogContent = content;

            int result = db.SaveChanges();

            String message = result > 0 ? "Updating successful!" : "Updating failed!";
            Console.WriteLine(message);
        }

        private void Delete(int id)
        {
            var item = db.Blogs.FirstOrDefault(x => x.BlogId == id); 
            if (item is null)
            {
                Console.WriteLine("No data found");
                return;
            }

            db.Blogs.Remove(item);
            int result = db.SaveChanges();

            String message = result > 0 ? "Deleting successful!" : "Deleting failed!";
            Console.WriteLine(message);

        }
    }
}

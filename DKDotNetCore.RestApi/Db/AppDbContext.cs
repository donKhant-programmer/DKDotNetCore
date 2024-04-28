using DKDotNetCore.RestApi.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DKDotNetCore.RestApi.Db
{
    internal class AppDbContext : DbContext
    {
        //shortcut - override onc
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);

        }

        public DbSet<BlogModel> Blogs { get; set; }
    }
}

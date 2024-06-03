namespace DKDotNetCore.DataAccess.Db
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

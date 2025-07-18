using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace TodoApp.Infrastructure.Persistence
{
    public class TodoDbContextFactory : IDesignTimeDbContextFactory<TodoDbContext>
    {
        public TodoDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json") // لازم يكون ملف appsettings موجود في مشروع WebAPI
                .Build();

            var optionsBuilder = new DbContextOptionsBuilder<TodoDbContext>();
            optionsBuilder.UseSqlServer(config.GetConnectionString("DefaultConnection"));

            return new TodoDbContext(optionsBuilder.Options);
        }
    }
}

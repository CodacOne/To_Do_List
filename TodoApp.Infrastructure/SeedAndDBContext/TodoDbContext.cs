using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;


namespace TodoApp.Infrastructure.Persistence
{
    public class TodoDbContext : DbContext
    {
        public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
        {
        }


        public DbSet<clsTask> Task { get; set; }
        public DbSet<clsUser> User { get; set; }
       
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<clsTask>()
                .Property(t => t.Title)
                .IsRequired();
            DbInitializer.Seed(modelBuilder);

            // بيانات Seed كمثال
            //modelBuilder.Entity<clsUser>().HasData(
            //    new clsUser { Id = "1", Username = "owner", PasswordHash = "hashedpassword", Role = "Owner" }
            //);
        }

    }
}

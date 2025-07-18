using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Infrastructure.Persistence
{
    public static class DbInitializer
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            // إنشاء مستخدمين
            var user1 = new clsUser
            {
                Id = "user-1",
                Username = "admin",
                Email = "admin@example.com",
                PasswordHash = "admin-hash", // استخدم Hash فعلي في الإنتاج
                Role = "Owner"
            };

            var user2 = new clsUser
            {
                Id = "user-2",
                Username = "guest",
                Email = "guest@example.com",
                PasswordHash = "guest-hash",
                Role = "Guest"
            };

            modelBuilder.Entity<clsUser>().HasData(user1, user2);

            // إنشاء مهام مبدئية
            modelBuilder.Entity<clsTask>().HasData(
                new clsTask
                {
                    Id = 1,
                    Title = "Setup project",
                    Description = "Create project structure and configure dependencies.",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(3),
                    Priority = 1,
                    Category = "Development",
                    OwnerId = "user-1"
                },
                new clsTask
                {
                    Id = 2,
                    Title = "Write documentation",
                    Description = "Document the API endpoints.",
                    IsCompleted = false,
                    CreatedAt = DateTime.UtcNow,
                    DueDate = DateTime.UtcNow.AddDays(5),
                    Priority = 2,
                    Category = "Docs",
                    OwnerId = "user-2"
                }
            );
        }
    }
}

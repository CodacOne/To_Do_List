using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using TodoApp.Infrastructure;

namespace TodoApp.Infrastructure.Repositories
{
    public class TaskRepository : ITaskRepository
    {
        private readonly TodoDbContext _context;

        public TaskRepository(TodoDbContext context)
        {
            _context = context;
        }

        public IQueryable<clsTask> GetAll()
        {
            return _context.Task.AsQueryable();
        }

       


        public async Task<IEnumerable<clsTask>> GetAllAsync()
        {
            return await _context.Task.ToListAsync();
        }

        public async Task<clsTask?> GetByIdAsync(int id)
        {
            return await _context.Task.FindAsync(id);
        }

        public async Task AddAsync(clsTask task)
        {
            await _context.Task.AddAsync(task);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(clsTask task)
        {
            _context.Task.Update(task);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var task = await _context.Task.FindAsync(id);
            if (task != null)
            {
                _context.Task.Remove(task);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<IEnumerable<clsTask>> SearchAsync(string keyword)
        {
            return await _context.Task
                .Where(t => t.Title.Contains(keyword) || t.Description.Contains(keyword))
                .ToListAsync();
        }

        public IQueryable<clsTask> Query()
        {
            return _context.Task.AsQueryable(); // أو DbSet اسم الجدول
        }
    }

}

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;
using TodoApp.Infrastructure.Persistence;

namespace TodoApp.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TodoDbContext _context;

        public UserRepository(TodoDbContext context)
        {
            _context = context;
        }

        public async Task<clsUser?> GetByIdAsync(string id)
        {
            return await _context.User.FindAsync(id);
        }

        public async Task<clsUser?> GetByUsernameAsync(string username)
        {
            return await _context.User.FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task AddAsync(clsUser user)
        {
            await _context.User.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(clsUser user)
        {
            _context.User.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(string id)
        {
            var user = await _context.User.FindAsync(id);
            if (user != null)
            {
                _context.User.Remove(user);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<bool> ValidateCredentialsAsync(string username, string password)
        {
            var user = await _context.User.FirstOrDefaultAsync(u => u.Username == username);
            if (user == null) return false;

          
            return user.PasswordHash == password; 
        }


        public async Task<IEnumerable<clsUser>> GetAllUsersAsync()
        {
            return await _context.User.ToListAsync();
        }

    }


}

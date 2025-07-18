using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
    // واجهة عامة للتعامل مع المستخدمين
    public interface IUserRepository
    {
        Task<clsUser?> GetByIdAsync(string id);
        Task<clsUser?> GetByUsernameAsync(string username);
        Task AddAsync(clsUser user);
        Task UpdateAsync(clsUser user);
        Task DeleteAsync(string id);
        Task<IEnumerable<clsUser>> GetAllUsersAsync();

        Task<bool> ValidateCredentialsAsync(string username, string password);
    }



}

using TodoApp.Application.Models;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Interfaces
{
    public interface ITaskRepository
    {
        Task<IEnumerable<clsTask>> GetAllAsync();
        Task<clsTask?> GetByIdAsync(int id);
        Task AddAsync(clsTask task);
        Task UpdateAsync(clsTask task);
        Task DeleteAsync(int id);
        Task<IEnumerable<clsTask>> SearchAsync(string keyword);
        IQueryable<clsTask> Query();

        IQueryable<clsTask> GetAll();
        
    }
}

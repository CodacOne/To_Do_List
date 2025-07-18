using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;
using TodoApp.Application.Interfaces;
using TodoApp.Application.Models;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public interface ITaskService
    {
        Task<IEnumerable<clsTask>> GetAllTasksAsync();
        Task<clsTask?> GetTaskByIdAsync(int id);
        Task AddTaskAsync(clsTask task);
        Task UpdateTaskAsync(clsTask task);
        Task DeleteTaskAsync(int id);
        Task<IEnumerable<clsTask>> SearchTasksAsync(string keyword);
        Task<PagedResult<TaskDto>> GetTasksAsync(int pageNumber, int pageSize, string? title, bool? isCompleted, string? category, int? priority);


    }

    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly IMapper _mapper;
        public TaskService(ITaskRepository taskRepository , IMapper Mapper )
        {
            _taskRepository = taskRepository;
            _mapper = Mapper;
        }

        public async Task<IEnumerable<clsTask>> GetAllTasksAsync()
        {
            return await _taskRepository.GetAllAsync();
        }

        public async Task<clsTask?> GetTaskByIdAsync(int id)
        {
            return await _taskRepository.GetByIdAsync(id);
        }

        public async Task AddTaskAsync(clsTask task)
        {
            // هنا ممكن تضيف منطق إضافي قبل الإضافة
            await _taskRepository.AddAsync(task);
        }

        public async Task UpdateTaskAsync(clsTask task)
        {
            await _taskRepository.UpdateAsync(task);
        }

        public async Task DeleteTaskAsync(int id)
        {
            await _taskRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<clsTask>> SearchTasksAsync(string keyword)
        {
            return await _taskRepository.SearchAsync(keyword);
        }

        public async Task<PagedResult<TaskDto>> GetTasksAsync(int pageNumber, int pageSize, string? title, bool? isCompleted, string? category, int? priority)
        {
            var query = _taskRepository.Query(); 

            if (!string.IsNullOrEmpty(title))
                query = query.Where(t => t.Title.Contains(title));

            if (isCompleted.HasValue)
                query = query.Where(t => t.IsCompleted == isCompleted.Value);

            if (!string.IsNullOrEmpty(category))
                query = query.Where(t => t.Category == category);

            if ( priority != null)
                query = query.Where(t => t.Priority == priority);

            var totalCount = await query.CountAsync();

            var tasks = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var mappedTasks = _mapper.Map<List<TaskDto>>(tasks);

            return new PagedResult<TaskDto>(mappedTasks, totalCount, pageNumber, pageSize);
        }


    }


}

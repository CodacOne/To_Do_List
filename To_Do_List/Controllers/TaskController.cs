using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;
using TodoApp.Application.DTOs; /
using AutoMapper; 

namespace To_Do_List.Controllers
{
  //  [Authorize (Roles = "Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly ITaskService _taskService;
        private readonly IMapper _mapper; 

        public TaskController(ITaskService taskService, IMapper mapper)
        {
            _taskService = taskService;
            _mapper = mapper;
        }

        [Authorize(Roles = "Guest")]
        [HttpGet("GetAll", Name = "All")]
        public async Task<IActionResult> GetAll()
        {
            var tasks = await _taskService.GetAllTasksAsync();
            var taskDtos = _mapper.Map<IEnumerable<TaskDto>>(tasks); 
            return Ok(taskDtos);
        }

        [HttpGet("GetByID", Name = "ById")]
        [Authorize(Roles = "Guest")]
        public async Task<IActionResult> GetById(int id)
        {
            var task = await _taskService.GetTaskByIdAsync(id);
            if (task == null) return NotFound();

            var taskDto = _mapper.Map<TaskDto>(task); 
            return Ok(taskDto);
        }

        [HttpPost("Create", Name = "Create")]
        public async Task<IActionResult> Create(TaskDto taskDto) 
        {
            var task = _mapper.Map<clsTask>(taskDto); 
            await _taskService.AddTaskAsync(task);

            var createdDto = _mapper.Map<TaskDto>(task); 
            return CreatedAtAction(nameof(GetById), new { id = task.Id }, createdDto);
        }

        [HttpPut("Update", Name = "UpdateTask")]
        public async Task<IActionResult> Update(TaskDto taskDto) // ✅
        {
            var existingTask = await _taskService.GetTaskByIdAsync(taskDto.Id);
            if (existingTask == null) return NotFound();

            var task = _mapper.Map<clsTask>(taskDto); // تحويل للكيان
            await _taskService.UpdateTaskAsync(task);

            return NoContent();
        }

        [HttpDelete("Delete/{id}", Name = "DeleteTask")]
        public async Task<IActionResult> Delete(int id)
        {
            var existingTask = await _taskService.GetTaskByIdAsync(id);
            if (existingTask == null) return NotFound();

            await _taskService.DeleteTaskAsync(id);
            return NoContent();
        }

        [HttpGet("GetAllUsingFilterAndPagination")]
        [Authorize(Roles = "Guest,Owner")]
       
        public async Task<IActionResult> GetAll(
         [FromQuery] int pageNumber = 1,
         [FromQuery] int pageSize = 10,
         [FromQuery] string? title = null,
         [FromQuery] bool? isCompleted = null,
         [FromQuery] string? category = null,
         [FromQuery] int? priority = null)
        {
            var pagedTasks = await _taskService.GetTasksAsync(pageNumber, pageSize, title, isCompleted, category, priority);
            return Ok(pagedTasks);
        }

    }
}

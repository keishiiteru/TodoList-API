using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Application.Services;
using TodoList.Contracts.DTOs;
using TodoList.Domain.Services;

namespace TodoList.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "User")]
    public class TasksController : Controller
    {
        private readonly ITaskService _taskService;

        public TasksController(ITaskService taskService)
        {
            _taskService = taskService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTask([FromBody] CreateTaskDto request)
        {
            try
            {
                var result = await _taskService.CreateTask(request);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllTasks()
        {
            try
            {
                var result = await _taskService.GetAllTasks();
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{taskId}")]
        public async Task<IActionResult> GetTaskById(int taskId)
        {
            try
            {
                var result = await _taskService.GetTaskById(taskId);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{taskId}")]
        public async Task<IActionResult> UpdateTask(int taskId, [FromBody] UpdateTaskDto request)
        {
            try
            {
                var result = await _taskService.UpdateTask(taskId, request);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{taskId}")]
        public async Task<IActionResult> DeleteTask(int taskId)
        {
            try
            {
                var result = await _taskService.DeleteTask(taskId);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.DTOs;
using TodoList.Contracts.ViewModels;

namespace TodoList.Domain.Services
{
    public interface ITaskService
    {
        Task<string> CreateTask(CreateTaskDto request);
        Task<IEnumerable<TaskVM>> GetAllTasks();
        Task<TaskVM> GetTaskById(int taskId);
        Task<string> UpdateTask(int taskId, UpdateTaskDto request);
        Task<string> DeleteTask(int taskId);
    }
}

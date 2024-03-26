using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.DTOs;
using TodoList.Contracts.ViewModels;
using TodoList.Domain.Entities;
using TodoList.Domain.Services;
using TodoList.Domain.UnitOfWork;
using Task = TodoList.Domain.Entities.Task;

namespace TodoList.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly ITodoManager _todoManager;
        private readonly IUserService _userService;

        public TaskService(ITodoManager todoManager, IUserService userService)
        {
            _todoManager = todoManager;
            _userService = userService;
        }

        public async Task<string> CreateTask(CreateTaskDto request)
        {
            try
            {
                var task = new Task
                {
                    UserId = _userService.GetMyProfile().UserId,
                    PriorityId = request.PriorityId,
                    CategoryId = request.CategoryId,
                    Title = request.Title,
                    Description = request.Description,
                    IsCompleted = request.IsCompleted,
                    Deadline = request.Deadline
                };

                _todoManager.TaskRepository.Insert(task);
                _todoManager.Save();

                return "Task created successfully!";

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<TaskVM>> GetAllTasks()
        {
            try
            {
                var result = _todoManager.TaskRepository
                                         .GetQueryableWithInclude(
                                            x => !x.IsDeleted && x.UserId == _userService.GetMyProfile().UserId,
                                            task => task.User,
                                            task => task.Category,
                                            task => task.Priority)
                                         .Select(x => new TaskVM
                                         {
                                             TaskId = x.TaskId,
                                             Title = x.Title,
                                             Description = x.Description,
                                             IsCompleted = x.IsCompleted,
                                             Deadline = x.Deadline,
                                             Priority = x.Priority.Name,
                                             Category = x.Category.Name
                                         })
                                         .ToList();

                return result ?? Enumerable.Empty<TaskVM>();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<TaskVM> GetTaskById(int taskId)
        {
            try
            {
                var result = _todoManager.TaskRepository
                                         .GetQueryableWithInclude(
                                            x => !x.IsDeleted &&
                                                  x.UserId == _userService.GetMyProfile().UserId &&
                                                  x.TaskId == taskId,
                                            task => task.User,
                                            task => task.Category,
                                            task => task.Priority)
                                         .Select(x => new TaskVM
                                         {
                                             TaskId = x.TaskId,
                                             Title = x.Title,
                                             Description = x.Description,
                                             IsCompleted = x.IsCompleted,
                                             Deadline = x.Deadline,
                                             Priority = x.Priority.Name,
                                             Category = x.Category.Name
                                         })
                                         .FirstOrDefault();

                return result ?? new TaskVM();

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> UpdateTask(int taskId, UpdateTaskDto request)
        {
            var task = _todoManager.TaskRepository
                                   .GetQueryable(x => !x.IsDeleted &&
                                                       x.UserId == _userService.GetMyProfile().UserId &&
                                                       x.TaskId == taskId)
                                   .FirstOrDefault();
            if (task != null)
            {
                task.PriorityId = request.PriorityId == 0 ? task.PriorityId : request.PriorityId;
                task.CategoryId = request.CategoryId == 0 ? task.CategoryId : request.CategoryId;
                task.Title = request.Title == string.Empty ? task.Title : request.Title;
                task.Description = request.Description == string.Empty ? task.Description : request.Description;
                task.IsCompleted = (bool) (request.IsCompleted == null ? task.IsCompleted : request.IsCompleted);
                task.Deadline = request.Deadline == null ? task.Deadline : request.Deadline;
                task.UpdatedAt = DateTime.Now;

                _todoManager.TaskRepository.Update(task);
                _todoManager.Save();

                return "Task info updated successfully!";
            }

            return "Task info not found!";

        }

        public async Task<string> DeleteTask(int taskId)
        {
            var task = _todoManager.TaskRepository
                                   .GetQueryable(x => !x.IsDeleted &&
                                                       x.UserId == _userService.GetMyProfile().UserId &&
                                                       x.TaskId == taskId)
                                   .FirstOrDefault();

            if (task != null)
            {
                task.IsDeleted = true;
                task.UpdatedAt = DateTime.Now;

                _todoManager.TaskRepository.Update(task);
                _todoManager.Save();

                return "Task info deleted successfully!";
            }

            return "Task info not found!";
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;
using TodoList.Domain.UnitOfWork;
using TodoList.Infrastructure.Repositories;
using Task = TodoList.Domain.Entities.Task;

namespace TodoList.Infrastructure.UnitOfWork
{
    public class TodoManager : ITodoManager
    {
        private TodoDbContext _context;

        private IGenericRepository<Category> categoryRepository;
        private IGenericRepository<Priority> priorityRepository;
        private IGenericRepository<Task> taskRepository;
        private IGenericRepository<User> userRepository;

        public TodoManager(TodoDbContext context) 
        {
            _context = context;
        }

        public IGenericRepository<Category> CategoryRepository
        {
            get
            {
                if (categoryRepository == null)
                {
                    categoryRepository = new GenericRepository<Category>(_context);
                }
                return categoryRepository;
            }
        }

        public IGenericRepository<Priority> PriorityRepository
        {
            get
            {
                if (priorityRepository == null)
                {
                    priorityRepository = new GenericRepository<Priority>(_context);
                }
                return priorityRepository;
            }
        }

        public IGenericRepository<Task> TaskRepository
        {
            get
            {
                if (taskRepository == null)
                {
                    taskRepository = new GenericRepository<Task>(_context);
                }
                return taskRepository;
            }
        }

        public IGenericRepository<User> UserRepository
        {
            get
            {
                if (userRepository == null)
                {
                    userRepository = new GenericRepository<User>(_context);
                }
                return userRepository;
            }
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}

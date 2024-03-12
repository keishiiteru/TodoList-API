using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;
using TodoList.Domain.Repositories;
using Task = TodoList.Domain.Entities.Task;

namespace TodoList.Domain.UnitOfWork
{
    public interface ITodoManager : IDisposable
    {
        void DataConfiguration();
        IGenericRepository<Category> CategoryRepository { get; }
        IGenericRepository<Priority> PriorityRepository { get; }
        IGenericRepository<Task> TaskRepository { get; }
        IGenericRepository<User> UserRepository { get; }
        void Save();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.ViewModels;

namespace TodoList.Domain.Services
{
    public interface IPriorityService
    {
        Task<IEnumerable<PriorityVM>> GetPriorities();
        Task<PriorityVM> GetPriorityById(int priorityId);
    }
}

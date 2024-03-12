using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Domain.Entities;

namespace TodoList.Infrastructure.Configuration
{
    public class TodoConfiguration
    {
        public IEnumerable<Priority> GetPriorities()
        {
            var priorities = new List<Priority>
            {
                new Priority {PriorityId = 1, Name = "Low" },
                new Priority {PriorityId = 2, Name = "Medium" },
                new Priority {PriorityId = 3, Name = "High" },
                new Priority {PriorityId = 4, Name = "Urgent"}
            };

            return priorities;
        }
    }
}

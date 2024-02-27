using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Entities
{
    public class Task : BaseEntity
    {
        public int TaskId { get; set; }
        public int UserId { get; set; }
        public int PriorityId { get; set; }
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime? Deadline { get; set; }
        public User User { get; set; }
        public Category Category { get; set; }
        public Priority Priority { get; set; }
    }
}

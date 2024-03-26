using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Contracts.ViewModels
{
    public class TaskVM
    {
        public int TaskId { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public DateTime? Deadline { get; set; }
        public string Priority { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
    }
}

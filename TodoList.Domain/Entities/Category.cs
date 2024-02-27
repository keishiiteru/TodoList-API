using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Entities
{
    public class Category : BaseEntity
    {
        public int CategoryId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public User User { get; set; }
        public List<Task> Tasks { get; set; }
    }
}

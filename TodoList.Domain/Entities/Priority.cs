﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoList.Domain.Entities
{
    public class Priority : BaseEntity
    {
        public int PriorityId { get; set; }
        public string Name { get; set; } = string.Empty;
        public List<Task> Tasks { get; set; }
    }
}

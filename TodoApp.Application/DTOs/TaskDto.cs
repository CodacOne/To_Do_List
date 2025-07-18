using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.DTOs
{
    public class TaskDto
    {

        public int Id { get; set; }
        public string Title { get; set; }
        public string Status { get; set; }
        public string UserId { get; set; }

    }
}

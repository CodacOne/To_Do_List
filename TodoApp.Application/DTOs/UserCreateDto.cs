using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.DTOs
{
    public class UserCreateDto
    {
        [Required]
        public string Username { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
}

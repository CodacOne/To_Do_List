﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TodoApp.Application.DTOs
{
    public class UserUpdateDto
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Role { get; set; }
    }
}

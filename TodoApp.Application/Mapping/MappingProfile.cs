using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using System.Threading.Tasks;
using TodoApp.Application.DTOs;
using TodoApp.Domain.Entities;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TodoApp.Application.Mapping
{
    public class MappingProfile : Profile
    {


        public MappingProfile()
        {
            CreateMap<clsUser, UserDto>().ReverseMap();
            CreateMap<clsUser, UserCreateDto>().ReverseMap();
            CreateMap<clsUser, UserUpdateDto>().ReverseMap();
            CreateMap<clsTask, TaskDto>();
        }

    }
}

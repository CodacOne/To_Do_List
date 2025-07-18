using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TodoApp.Application.DTOs;
using TodoApp.Application.Services;
using TodoApp.Domain.Entities;

namespace To_Do_List.Controllers
{

    [Authorize(Roles = "Owner")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUserService _userService;

        public UserController(IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _mapper = mapper;
        }

        
        [HttpGet("GetAll", Name = "AllUsers")]
        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return _mapper.Map<IEnumerable<UserDto>>(users);
        }

        [HttpGet("GetByID", Name = "GetUserById")]
        public async Task<IActionResult> GetById(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            var userDto = _mapper.Map<UserDto>(user);
            return Ok(userDto);
        }

        [HttpPost("Create", Name = "CreateUser")]
        public async Task<IActionResult> Create(UserCreateDto userCreateDto)
        {
            var user = _mapper.Map<clsUser>(userCreateDto);
            await _userService.AddUserAsync(user);
            var userDto = _mapper.Map<UserDto>(user);

            return CreatedAtAction(nameof(GetById), new { id = user.Id }, userDto);
        }

        [HttpPut("Update", Name = "UpdateUser")]
        public async Task<IActionResult> Update(UserUpdateDto userUpdateDto)
        {
            var existingUser = await _userService.GetUserByIdAsync(userUpdateDto.Id);
            if (existingUser == null) return NotFound();

            var updatedUser = _mapper.Map(userUpdateDto, existingUser);
            await _userService.UpdateUserAsync(updatedUser);
            return NoContent();
        }

        [HttpDelete("Delete/{id}", Name = "DeleteUser")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingUser = await _userService.GetUserByIdAsync(id);
            if (existingUser == null) return NotFound();

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }

}

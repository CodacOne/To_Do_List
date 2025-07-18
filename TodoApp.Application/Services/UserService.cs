using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoApp.Application.Interfaces;
using TodoApp.Domain.Entities;

namespace TodoApp.Application.Services
{
    public interface IUserService
    {
        Task<clsUser?> GetUserByIdAsync(string id);
        Task<clsUser?> GetUserByUsernameAsync(string username);
        Task AddUserAsync(clsUser user);
        Task UpdateUserAsync(clsUser user);
        Task DeleteUserAsync(string id);
        Task<bool> ValidateUserCredentialsAsync(string username, string password);

        Task<IEnumerable<clsUser>> GetAllUsersAsync();

    }

    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        

        public async Task<IEnumerable<clsUser>> GetAllUsersAsync()
        {
          
            return await _userRepository.GetAllUsersAsync();
        }


        public async Task<clsUser?> GetUserByIdAsync(string id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        public async Task<clsUser?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        public async Task AddUserAsync(clsUser user)
        {
            //
            await _userRepository.AddAsync(user);
        }

        public async Task UpdateUserAsync(clsUser user)
        {
            await _userRepository.UpdateAsync(user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _userRepository.DeleteAsync(id);
        }
      

        public async Task<bool> ValidateUserCredentialsAsync(string username, string password)
        {
            return await _userRepository.ValidateCredentialsAsync(username, password);
        }
    }

}

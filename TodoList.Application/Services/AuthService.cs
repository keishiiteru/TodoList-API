using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.DTOs;
using TodoList.Domain.Entities;
using TodoList.Domain.Services;
using TodoList.Domain.UnitOfWork;

namespace TodoList.Application.Services
{
    public class AuthService : IAuthService
    {
        private readonly ITodoManager _manager;

        public AuthService(ITodoManager manager)
        {
            _manager = manager;
        }
        public async Task<User> Register(RegisterDto request)
        {
            try
            {
                CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);

                User user = new User
                {
                    Username = request.Username,
                    Email = request.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt
                };

                _manager.UserRepository.Insert(user);
                _manager.Save();

                return user;
            }

            catch (Exception)
            {
                throw;
            }
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using (var hmac = new HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}

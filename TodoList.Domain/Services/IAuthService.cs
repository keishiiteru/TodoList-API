using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.DTOs;
using TodoList.Domain.Entities;

namespace TodoList.Domain.Services
{
    public interface IAuthService
    {
        Task<User> Register(RegisterDto request);
        Task<string> Login(LoginDto request);
    }
}

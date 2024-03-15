using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.ViewModels;
using TodoList.Domain.Services;

namespace TodoList.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public UserProfileVM GetMyProfile()
        {
            var result = new UserProfileVM();

            if (_httpContextAccessor.HttpContext != null)
            {
                result = new UserProfileVM
                {
                    UserId = Convert.ToInt32(_httpContextAccessor.HttpContext.User.FindFirstValue("UserId")),
                    UserName = _httpContextAccessor.HttpContext.User.FindFirstValue("Username") ?? string.Empty,
                    Role = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.Role) ?? string.Empty
                };
            }

            return result;
        }
    }
}

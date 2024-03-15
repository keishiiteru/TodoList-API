using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.ViewModels;

namespace TodoList.Domain.Services
{
    public interface IUserService
    {
        public UserProfileVM GetMyProfile();
    }
}

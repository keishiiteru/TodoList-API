using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.ViewModels;
using TodoList.Domain.Entities;
using TodoList.Domain.Services;
using TodoList.Domain.UnitOfWork;

namespace TodoList.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ITodoManager _todoManager;
        private readonly IUserService _userService;

        public CategoryService(ITodoManager todoManager, IUserService userService)
        {
            _todoManager = todoManager;
            _userService = userService;
        }

        public async Task<string> CreateCategory(string categoryName)
        {
            try
            {
                var category = new Category
                {
                    UserId = _userService.GetMyProfile().UserId,
                    Name = categoryName
                };

                _todoManager.CategoryRepository.Insert(category);
                _todoManager.Save();

                return "Category created successfully!";
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<IEnumerable<CategoryVM>> GetAllCategories()
        {
            try
            {
                var result = _todoManager.CategoryRepository
                                     .GetQueryable(x => !x.IsDeleted &&
                                                         x.UserId == _userService.GetMyProfile().UserId)
                                     .Select(x => new CategoryVM
                                     {
                                         CategoryId = x.CategoryId,
                                         CategoryName = x.Name
                                     })
                                     .ToList();

                return result ?? Enumerable.Empty<CategoryVM>();
            }
            catch (Exception)
            {

                throw;
            }
            
        }

        public async Task<CategoryVM> GetCategoryById(int categoryId)
        {
            try
            {
                var result = _todoManager.CategoryRepository
                                         .GetQueryable(x => !x.IsDeleted &&
                                                             x.UserId == _userService.GetMyProfile().UserId &&
                                                             x.CategoryId == categoryId)
                                         .Select(x => new CategoryVM
                                         {
                                             CategoryId = x.CategoryId,
                                             CategoryName = x.Name
                                         })
                                         .FirstOrDefault();

                return result ?? new CategoryVM();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<string> UpdateCategory(int categoryId, string categoryName)
        {
            var category = _todoManager.CategoryRepository
                                       .GetQueryable(x => !x.IsDeleted &&
                                                           x.UserId == _userService.GetMyProfile().UserId &&
                                                           x.CategoryId == categoryId)
                                       .FirstOrDefault();

            if (category != null)
            {
                category.Name = categoryName;
                category.UpdatedAt = DateTime.Now;

                _todoManager.CategoryRepository.Update(category);
                _todoManager.Save();

                return "Category info updated successfully!";
            }

            return "Category info not found!";
        }

        public async Task<string> DeleteCategory(int categoryId)
        {
            var category = _todoManager.CategoryRepository
                                       .GetQueryable(x => !x.IsDeleted &&
                                                           x.UserId == _userService.GetMyProfile().UserId &&
                                                           x.CategoryId == categoryId)
                                       .FirstOrDefault();

            if (category != null)
            {
                category.IsDeleted = true;
                category.UpdatedAt = DateTime.Now;

                _todoManager.CategoryRepository.Update(category);
                _todoManager.Save();

                return "Category info deleted successfully!";
            }

            return "Category info not found!";
        }


    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TodoList.Contracts.ViewModels;

namespace TodoList.Domain.Services
{
    public interface ICategoryService
    {
        Task<string> CreateCategory(string categoryName);
        Task<IEnumerable<CategoryVM>> GetAllCategories();
        Task<CategoryVM> GetCategoryById(int categoryId);
        Task<string> UpdateCategory(int categoryId, string categoryName);
        Task<string> DeleteCategory(int categoryId);
    }
}

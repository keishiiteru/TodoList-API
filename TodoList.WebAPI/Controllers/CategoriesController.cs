using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Services;

namespace TodoList.WebAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "User")]
    public class CategoriesController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategory([FromBody] string categoryName)
        {
            try
            {
                var result = await _categoryService.CreateCategory(categoryName);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            try
            {
                var result = await _categoryService.GetAllCategories();
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpGet("{categoryId}")]
        public async Task<IActionResult> GetCategoryById(int categoryId)
        {
            try
            {
                var result = await _categoryService.GetCategoryById(categoryId);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpPut("{categoryId}")]
        public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] string categoryName)
        {
            try
            {
                var result = await _categoryService.UpdateCategory(categoryId, categoryName);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [HttpDelete("{categoryId}")]
        public async Task<IActionResult> DeleteCategory(int categoryId)
        {
            try
            {
                var result = await _categoryService.DeleteCategory(categoryId);
                return Ok(result);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}

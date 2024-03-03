using Microsoft.AspNetCore.Mvc;

namespace TodoList.WebAPI.Controllers
{
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

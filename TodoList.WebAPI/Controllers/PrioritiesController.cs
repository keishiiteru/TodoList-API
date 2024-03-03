using Microsoft.AspNetCore.Mvc;

namespace TodoList.WebAPI.Controllers
{
    public class PrioritiesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

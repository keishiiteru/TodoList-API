using Microsoft.AspNetCore.Mvc;

namespace TodoList.WebAPI.Controllers
{
    public class TasksController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

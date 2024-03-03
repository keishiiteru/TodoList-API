using Microsoft.AspNetCore.Mvc;

namespace TodoList.WebAPI.Controllers
{
    public class AuthController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}

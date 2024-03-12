using Azure.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TodoList.Domain.Services;
using TodoList.Domain.UnitOfWork;

namespace TodoList.WebAPI.Controllers
{

    [Route("api/[controller]/[action]")]
    [Authorize(Roles = "User")]
    [ApiController]
    public class PrioritiesController : Controller
    {
        private readonly ITodoManager _manager;
        private readonly IPriorityService _priorityService;

        public PrioritiesController(IPriorityService priorityService)
        {
            _priorityService = priorityService;
        }

        [HttpGet]
        public async Task<IActionResult> GetPriorities()
        {
            try
            {
                var result = await _priorityService.GetPriorities();
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetPriorityById(int priorityId) 
        {
            try
            {
                var result = await _priorityService.GetPriorityById(priorityId);
                return Ok(result);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

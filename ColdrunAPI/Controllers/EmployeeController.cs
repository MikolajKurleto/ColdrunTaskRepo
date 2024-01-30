using Microsoft.AspNetCore.Mvc;

namespace ColdrunAPI.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly ILogger<EmployeeController> _logger;

        public EmployeeController(ILogger<EmployeeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Employee()
        {
            return View();
        }
    }
}

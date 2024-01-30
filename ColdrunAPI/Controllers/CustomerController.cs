using Microsoft.AspNetCore.Mvc;

namespace ColdrunAPI.Controllers
{
    public class CustomerController : Controller
    {
        private readonly ILogger<CustomerController> _logger;

        public CustomerController(ILogger<CustomerController> logger)
        {
            _logger = logger;
        }

        public IActionResult Customer()
        {
            return View();
        }
    }
}

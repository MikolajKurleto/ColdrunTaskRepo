using Microsoft.AspNetCore.Mvc;

namespace ColdrunAPI.Controllers
{
    public class FactoryController : Controller
    {
        private readonly ILogger<FactoryController> _logger;

        public FactoryController(ILogger<FactoryController> logger)
        {
            _logger = logger;
        }

        public IActionResult Factory()
        {
            return View();
        }
    }
}

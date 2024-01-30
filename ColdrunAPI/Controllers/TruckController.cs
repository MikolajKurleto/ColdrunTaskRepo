using ColdrunAPI.Models;
using ColdrunAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ColdrunAPI.Controllers
{
    public class TruckController : Controller
    {
        private readonly TruckService _service;
        private readonly TruckListModel _model;
        private readonly ILogger<TruckController> _logger;

        public TruckController(ILogger<TruckController> logger, TruckService service, TruckListModel model)
        {
            _logger = logger;
            _service = service;
            _model = model;
        }

        public IActionResult Truck()
        {
            GetTruckList();
            return View(_model);
        }

        public IActionResult SortBy(bool sortASC, string sortName)
        {
            string name = sortASC ? sortName : $"{sortName}_desc";
            _model.Trucks = _service.SortBy(name, _model.Trucks);
            _model.SortASC = !sortASC;
            return View("Truck", _model);
        }

        public IActionResult FindBy(string searchString) 
        {
            GetTruckList();

            if (string.IsNullOrEmpty(searchString)) 
            {
                return View("Truck", _model);
            }

            var searchStringLowered = searchString.ToLower();

            var searchResult = _model.Trucks
                .Where(t => t.Code.ToLower().Contains(searchStringLowered)
                    || t.Name.ToLower().Contains(searchStringLowered)
                    || t.Status.ToString().ToLower().Contains(searchStringLowered)
                    || (t.Description is not null ? t.Description.ToLower().Contains(searchStringLowered) : false))
                .ToList();

            _model.Trucks = searchResult;
            return View("Truck", _model);
        }

        [HttpGet("[controller]/GetAll")]
        public IActionResult GetTruckList()
        {
            _model.Trucks = _service.GetTrucks();
            return Ok(_model.Trucks);
        }

        [HttpGet("[controller]/GetTruck/{id}")]
        public IActionResult GetTruck(int id)
        {
            var truck = _service.GetTrucks().FirstOrDefault(i => i.Id == id);

            if (truck is null) 
            {
                return NotFound();
            }

            return Ok(truck);
        }

        [HttpPost("[controller]/Create")]
        public IActionResult Create(TruckModel newTruck)
        {
            bool useHtml = Request.Headers.Accept.ToString().Contains("text/html");
            if (!ModelState.IsValid || newTruck is null)
            {
                if (useHtml) 
                { 
                    return RedirectToAction("Truck");
                } 
                else 
                {
                    return BadRequest(ModelState);
                }
            }

            try
            { 
                _service.AddTruck(newTruck);
            } 
            catch (Exception ex)
            {
                if (useHtml)
                {
                    return RedirectToAction("ErrorWithReason", "Error", new { reason = ex.InnerException?.Message });
                }
                else
                {
                    return BadRequest(ex.InnerException?.Message);
                }
            }

            if (useHtml)
            {
                return RedirectToAction("Truck");
            }
            else
            {
                var createdTruckId = newTruck.Id;
                var uri = Url.Action("GetTruck", "Truck", new { id = createdTruckId }, Request.Scheme);
                return Created(uri, newTruck);
            }
        }

        [HttpPost("[controller]/Update/{id}")]
        public IActionResult Update(int id, TruckModel truck)
        {
            bool useHtml = Request.Headers.Accept.ToString().Contains("text/html");

            var model = _model.Trucks.FirstOrDefault(truck => truck.Id == id);

            if (model is null)
            {
                if (useHtml) 
                { 
                    return RedirectToAction("Truck");
                } 
                else 
                { 
                    return NotFound(id); 
                }
            }

            if (useHtml)
            {
                return View("TruckUpdate", model);
            }
            else
            {
                if (truck is null) 
                {
                    return BadRequest("Empty body");
                }

                try 
                {
                    _service.UpdateTruck(id, truck);
                }
                catch (Exception ex) 
                {
                    if (useHtml)
                    {
                        return RedirectToAction("ErrorWithReason", "Error", new { reason = ex.InnerException?.Message });
                    }
                    else
                    {
                        return BadRequest(ex.InnerException?.Message);
                    }
                }

                return Ok(truck);
            }
        }

        public IActionResult UpdateExecute(int id, TruckModel truck)
        {
            try
            {
                _service.UpdateTruck(id, truck);
            }
            catch (Exception ex)
            {              
                return RedirectToAction("ErrorWithReason", "Error", new { reason = ex.InnerException?.Message });               
            }

            return RedirectToAction("Truck");
        }

        [HttpPost("[controller]/Delete/{id}")]
        public IActionResult Delete(int id)
        {
            bool useHtml = Request.Headers.Accept.ToString().Contains("text/html");

            try 
            { 
                _service.DeleteTruck(id);
            }
            catch (Exception ex)
            {
                if (useHtml)
                {
                    return RedirectToAction("ErrorWithReason", "Error", new { reason = ex.InnerException?.Message });
                }
                else
                {
                    return BadRequest(ex.InnerException?.Message);
                }
            }

            if (useHtml)
            {
                return RedirectToAction("Truck");
            }
            else 
            {
                return NoContent();
            }
        }
    }
}

using ColdrunAPI.Data;
using ColdrunAPI.Models;

namespace ColdrunAPI.Services
{
    public class TruckService
    {
        private readonly ColdrunContext _context;
        public TruckService(ColdrunContext context)
        {
            _context = context;
        }

        public IList<TruckModel> GetTrucks() 
        {
            if (_context.Trucks is not null) 
            {
                return _context.Trucks.ToList();
            }
            return new List<TruckModel>();
        }

        public void AddTruck(TruckModel truck) 
        {
            if (_context.Trucks is not null) 
            {
                _context.Trucks.Add(truck);
                _context.SaveChanges();
            }
        }

        public void DeleteTruck(int id)
        {
            if (_context.Trucks is not null)
            {
                var truck = _context.Trucks.Find(id);
                if (truck is not null)
                {
                    _context.Trucks.Remove(truck);
                    _context.SaveChanges();
                }
            }
        }

        public void UpdateTruck(int id, TruckModel truckToUpdate)
        {
            if (_context.Trucks is not null) 
            {
                var truck = _context.Trucks.Find(id);
                if (truck is not null)
                {
                    _context.Trucks.Remove(truck);

                    var newTruck = new TruckModel()
                    {
                        Id = id,
                        Code = truckToUpdate.Code,
                        Name = truckToUpdate.Name,
                        Description = truckToUpdate.Description,
                        Status = truckToUpdate.Status,
                    };

                    _context.Trucks.Add(newTruck);
                    _context.SaveChanges();
                }
            }
        }

        internal IList<TruckModel> SortBy(string sortName, IList<TruckModel> trucks)
        {
            switch (sortName)
            {
                case "name":
                    return trucks.OrderBy(x => x.Name).ToList();
                case "name_desc":
                    return trucks.OrderByDescending(x => x.Name).ToList();
                case "code":
                    return trucks.OrderBy(x => x.Code).ToList();
                case "code_desc":
                    return trucks.OrderByDescending(x => x.Code).ToList();
                case "status":
                    return trucks.OrderBy(x => x.Status.ToString()).ToList();
                case "status_desc":
                    return trucks.OrderByDescending(x => x.Status.ToString()).ToList();
                case "description":
                    return trucks.OrderBy(x => x.Description).ToList();
                case "description_desc":
                    return trucks.OrderByDescending(x => x.Description).ToList();
                default:
                    return trucks;
            }          
        }
    }
}

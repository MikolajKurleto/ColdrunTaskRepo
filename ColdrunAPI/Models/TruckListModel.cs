namespace ColdrunAPI.Models
{
    public class TruckListModel
    {
        public IList<TruckModel> Trucks { get; set; } = new List<TruckModel>();
        public TruckModel NewTruck { get; set; } = default!;
        public bool SortASC { get; set; }
    }
}

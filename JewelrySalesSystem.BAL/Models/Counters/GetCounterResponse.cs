namespace JewelrySalesSystem.BAL.Models.Counters
{
    public class GetCounterResponse
    {
        public int CounterId { get; set; }
        public string CounterName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
    }
}

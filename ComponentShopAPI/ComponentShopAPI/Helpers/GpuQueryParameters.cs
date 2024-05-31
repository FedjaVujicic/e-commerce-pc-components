namespace ComponentShopAPI.Helpers
{
    public class GpuQueryParameters
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; } = "";
        public double PriceLow { get; set; } = 0;
        public double PriceHigh { get; set; } = 200000;
        public bool AvailableOnly { get; set; } = false;
        public string Slot { get; set; } = "";
        public int Memory { get; set; } = -1;
        public List<string> Ports { get; set; } = new List<string>();
    }
}

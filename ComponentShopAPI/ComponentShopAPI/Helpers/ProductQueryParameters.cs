namespace ComponentShopAPI.Helpers
{
    public class ProductQueryParameters
    {
        public string Category { get; set; } = "";
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public string Name { get; set; } = "";
        public double PriceLow { get; set; } = 0;
        public double PriceHigh { get; set; } = 200000;
        public bool AvailableOnly { get; set; } = false;
        public string Sort { get; set; } = "none";

        // Monitor specific parameters
        public double SizeLow { get; set; } = 0;
        public double SizeHigh { get; set; } = 100;
        public string Resolution { get; set; } = "";
        public int RefreshRate { get; set; } = -1;

        // Gpu specific parameters
        public string Slot { get; set; } = "";
        public int Memory { get; set; } = -1;
        public List<string> Ports { get; set; } = new List<string>();
    }
}

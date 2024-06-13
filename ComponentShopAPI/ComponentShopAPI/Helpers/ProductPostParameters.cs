namespace ComponentShopAPI.Helpers
{
    public class ProductPostParameters
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";
        public double Price { get; set; }
        public int Quantity { get; set; }
        public string ImageName { get; set; } = "";
        public IFormFile? ImageFile { get; set; }
        public string Category { get; set; } = "";

        // Monitor specific parameters
        public double Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }

        // Gpu specific parameters
        public string Slot { get; set; } = "";
        public int Memory { get; set; }
        public List<string> Ports { get; set; } = [];
    }
}

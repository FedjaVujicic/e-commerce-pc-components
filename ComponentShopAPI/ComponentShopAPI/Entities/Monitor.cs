namespace ComponentShopAPI.Entities
{
    public class Monitor : Product
    {
        public Monitor() { }
        public Monitor
            (int id, string name, double price, int quantity, string imageName, IFormFile? imageFile, double size, int width, int height, int refreshRate)
            : base(id, name, price, quantity, imageName, imageFile)
        {
            Size = size;
            Width = width;
            Height = height;
            RefreshRate = refreshRate;
        }

        public double Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }
    }
}
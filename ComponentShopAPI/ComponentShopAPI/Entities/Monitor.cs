namespace ComponentShopAPI.Entities
{
    public class Monitor : Product
    {
        public double Size { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int RefreshRate { get; set; }
    }
}
namespace ComponentShopAPI.Entities
{
    public class Gpu : Product
    {
        public string Slot { get; set; } = "";
        public int Memory { get; set; }
        public List<string> Ports { get; set; } = [];
    }
}

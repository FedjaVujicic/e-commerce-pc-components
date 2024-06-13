
namespace ComponentShopAPI.Entities
{
    public class Gpu : Product
    {
        public Gpu() { }
        public Gpu
            (int id, string name, double price, int quantity, string imageName, IFormFile? imageFile, string slot, int memory, List<string> ports)
            : base(id, name, price, quantity, imageName, imageFile)
        {
            Slot = slot;
            Memory = memory;
            Ports = ports;
        }

        public string Slot { get; set; } = "";
        public int Memory { get; set; }
        public List<string> Ports { get; set; } = [];
    }
}

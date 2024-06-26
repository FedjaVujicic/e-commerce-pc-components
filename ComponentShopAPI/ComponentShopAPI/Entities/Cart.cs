namespace ComponentShopAPI.Entities
{
    public class Cart
    {
        public int Id { get; set; }
        public string UserId { get; set; } = "";
        public ApplicationUser User { get; set; } = null!;
        public List<Product> Products { get; } = [];
        public List<CartProduct> CartProducts { get; } = [];
    }
}

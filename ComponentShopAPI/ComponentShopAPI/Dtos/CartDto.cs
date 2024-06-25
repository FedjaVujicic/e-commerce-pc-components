namespace ComponentShopAPI.Dtos
{
    public class CartDto
    {
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }

        public CartDto(ProductDto? productDto, int quantity)
        {
            Product = productDto;
            Quantity = quantity;
        }
    }
}

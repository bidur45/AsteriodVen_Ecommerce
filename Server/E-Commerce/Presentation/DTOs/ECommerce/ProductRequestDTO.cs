namespace Presentation.DTOs.ECommerce
{
    public class ProductRequestDTO
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public double? Rating { get; set; }
        public string? Img { get; set; }
    }
}

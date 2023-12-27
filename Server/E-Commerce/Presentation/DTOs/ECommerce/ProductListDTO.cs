namespace Presentation.DTOs.ECommerce
{
    public class ProductListDTO
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public int Stock { get; set; }
        public double Price { get; set; }
        public double? Rating { get; set; }
        public string? Img { get; set; }
    }
}

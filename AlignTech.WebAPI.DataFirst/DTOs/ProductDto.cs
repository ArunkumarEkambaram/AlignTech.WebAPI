namespace AlignTech.WebAPI.DataFirst.DTOs
{
    public class ProductDto
    {
        public string Id { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

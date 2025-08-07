namespace AlignTech.WebAPI.DataFirst.DTOs
{
    public class ProductAndCategoryDto
    {
        public byte CategoryId { get; set; }
        public string ProductName { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}

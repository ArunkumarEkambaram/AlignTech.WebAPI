namespace AlignTech.WebAPI.Day1.DTOs
{
    public class ProductDto
    {
        public string ProductName { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public bool IsInStock { get; set; }
        public string CategoryName { get; set; } = string.Empty;
        public DateTime AddedDate { get; set; }
    }
}

namespace AlignTech.WebAPI.DataFirst.DTOs
{
    public class AddProductDto
    {
        public string ProductId { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public byte? CategoryId { get; set; }

        public decimal Price { get; set; }

        public int QuantityAvailable { get; set; }
    }
}

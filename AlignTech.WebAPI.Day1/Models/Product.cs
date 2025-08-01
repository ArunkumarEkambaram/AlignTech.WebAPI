namespace AlignTech.WebAPI.Day1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public string? Description { get; set; }
        public decimal UnitPrice { get; set; }
        public int QuantityAvailable { get; set; }

        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}

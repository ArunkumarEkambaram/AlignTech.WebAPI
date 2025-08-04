namespace AlignTech.WebAPI.Day1.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Description { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int QuantityAvailable { get; set; }
        public bool StockAvailablility => QuantityAvailable > 0;
        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }

        //Reference Category
        public Category? Category { get; set; }

        //Naming convention for Creating Foreign Key (TableNameId or TableName_Id)
        public int? CategoryId { get; set; }
    }
}

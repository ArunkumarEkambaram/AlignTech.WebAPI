using System.ComponentModel.DataAnnotations;

namespace AlignTech.WebAPI.Day1.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Product Name cannot be empty")]
        [StringLength(100, MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500)]
        public string? Description { get; set; }

        [Range(1, 100000, MaximumIsExclusive = true)]
        [Display(Name = "Price")]
        public decimal UnitPrice { get; set; }

        [Display(Name = "Quantity")]
        [Range(0, 1000)]
        public int QuantityAvailable { get; set; }

        public bool StockAvailablility
        {
            get
            {
                if (QuantityAvailable <= 0)
                {
                    return false;
                }
                return true;
            }

        }

        public DateTime AddedDate { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }

    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [EmailAddress]
        [RegularExpression("")]
        public string EmailAddress { get; set; }
    }
}

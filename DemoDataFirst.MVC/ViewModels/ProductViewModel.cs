using System.ComponentModel.DataAnnotations;

namespace DemoDataFirst.MVC.ViewModels
{
    public class ProductViewModel
    {
        public string Id { get; set; } = null!;

        public string ProductName { get; set; } = null!;

        public string CategoryName { get; set; } = null!;

        public decimal Price { get; set; }

        public int Quantity { get; set; }
    }
}

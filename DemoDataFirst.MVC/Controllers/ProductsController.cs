using DemoDataFirst.MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace DemoDataFirst.MVC.Controllers
{
    public class ProductsController(Productservices productservices) : Controller
    {
        public async Task<IActionResult> Index()
        {
            var result = await productservices.GetProductsAsync();
            return View(result);
        }

        public IActionResult ProductByAjax()
        {
            return View();
        }
    }
}

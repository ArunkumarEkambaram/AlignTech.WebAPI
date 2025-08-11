using DemoDataFirst.MVC.ViewModels;
using Newtonsoft.Json;

namespace DemoDataFirst.MVC.Services
{
    public class Productservices(HttpClient client)
    {
        public async Task<IEnumerable<ProductViewModel>> GetProductsAsync()
        {
            List<ProductViewModel> products = new List<ProductViewModel>();
            var response = await client.GetAsync("https://localhost:7123/api/Products");
            if (response.IsSuccessStatusCode)
            {
                var jsonResult = await response.Content.ReadAsStringAsync();
                if (jsonResult != null)
                    products = JsonConvert.DeserializeObject<List<ProductViewModel>>(jsonResult);
            }
            return products ?? null!;
        }
    }
}

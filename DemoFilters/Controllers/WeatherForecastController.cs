using DemoFilters.Filters;
using Microsoft.AspNetCore.Mvc;

namespace DemoFilters.Controllers
{
    [ApiController]
    [Route("[controller]")]
    //[MyActionFilter]

    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        //[MyActionFilter("Hello from Action")]
        [TypeFilter(typeof(MyActionFilter), Arguments = ["Hello from Weatherforecast"])]
        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }

        [HttpGet("{id}")]
        public ActionResult<string> GetData(int id)
        {
            if (id == 0)
                throw new ArgumentException("Invalid argument passed");
            return Ok("Hello World");
        }
    }
}

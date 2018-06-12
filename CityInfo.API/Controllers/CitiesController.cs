using System.Linq;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        [HttpGet()]
        public IActionResult GetCities() {
            var cities = CityDataStore.Current.Cities;
            return Ok(cities);
        }

        [HttpGet(template: "{id}")]
        public IActionResult GetCity(int id) {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == id);
            if (city == null) return NotFound();
            
            return Ok(city);
        }        
    }
}
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Services;
using System.Collections.Generic;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class CitiesController : Controller
    {
        private ICityInfoRepo _cityInfoRepo;
        public CitiesController(ICityInfoRepo cityInfoRepo)
        {
            _cityInfoRepo = cityInfoRepo;
        }

        [HttpGet()]
        public IActionResult GetCities()
        {
            var cities = _cityInfoRepo.GetCities();
            var results = cities.ToDto();
            return Ok(results);
        }

        [HttpGet(template: "{id}")]
        public IActionResult GetCity(int id) {
            var city = _cityInfoRepo.GetCity(id);
            if (city == null) return NotFound();
            
            return Ok(city);
        }        
    }
}
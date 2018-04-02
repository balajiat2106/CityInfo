using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CityInfo.API.Models;
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

        [HttpGet("{cityId}/pointsofInterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
            if (city==null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterest);
        }

        [HttpGet("{cityId}/pointsofInterest/{id}",Name ="GetPointOfInterestBasedOnID")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
            if (city == null)
            {
                return NotFound();
            }
            return Ok(city.PointOfInterest.FirstOrDefault(p => p.id == id));
        }

        [HttpPost("{cityId}/pointsofinterest")]
        public IActionResult CreatePointOfInterest(int cityId, [FromBody] PointsOfInterestForCreationDTO pointofinterest)
        {
            if (pointofinterest == null) return BadRequest();

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var city = CityDataStore.Current.Cities.FirstOrDefault(c => c.id == cityId);
            if (city == null) return NotFound();

            var maxPointOfInterest = CityDataStore.Current.Cities.SelectMany(c => c.PointOfInterest).Max(p => p.id);

            var finalPointOfInterest = new PointsOFInterestDTO()
            {
                id = ++maxPointOfInterest,
                Name=pointofinterest.Name,
                Description=pointofinterest.Description
            };

            city.PointOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterestBasedOnID", new { cityId, finalPointOfInterest.id },finalPointOfInterest);
        }
    }
}
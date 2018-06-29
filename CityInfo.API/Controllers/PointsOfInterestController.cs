using System.Linq;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace CityInfo.API.Controllers
{
    [Route("api/cities")]
    public class PointsOfInterestController : Controller
    {
        private ILogger<PointsOfInterestController> _logger;
        private IMailService _mailService;
        private ICityInfoRepo _cityInfoRepo;

        //constructot injection
        public PointsOfInterestController(ILogger<PointsOfInterestController> logger,IMailService mailService, ICityInfoRepo cityInfoRepo)
        {
            _logger = logger;
            _mailService = mailService;
            _cityInfoRepo=cityInfoRepo;
        }

        [HttpGet("{cityId}/pointsofInterest")]
        public IActionResult GetPointsOfInterest(int cityId)
        {
            var city = _cityInfoRepo.GetCity(cityId);
            if (city == null)
            {
                _logger.LogInformation($"City with ID {cityId} cannot be found");
                return NotFound();
            }
            return Ok(city.PointOfInterest);
        }

        [HttpGet("{cityId}/pointsofInterest/{id}", Name = "GetPointOfInterestBasedOnID")]
        public IActionResult GetPointsOfInterest(int cityId, int id)
        {
            var city = _cityInfoRepo.GetCity(cityId);
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

            if (pointofinterest.Name == pointofinterest.Description)
            {
                ModelState.AddModelError("Description", "Name and Description should be identical");
            }

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
                Name = pointofinterest.Name,
                Description = pointofinterest.Description
            };

            city.PointOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute("GetPointOfInterestBasedOnID", new { cityId, finalPointOfInterest.id }, finalPointOfInterest);
        }

        [HttpPut("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointsOfInterest(int cityId, int id, [FromBody] PointsOfInterestForUpdationDTO pointsOfInterest)
        {
            if (pointsOfInterest == null)
            {
                return BadRequest();
            }

            var city = _cityInfoRepo.GetCity(cityId);
            if (city == null) { return NotFound(); }

            var pointofinterest = city.PointOfInterest.FirstOrDefault(p => p.id == id);
            if (pointofinterest == null) { return NotFound(); }

            pointofinterest.Name = pointsOfInterest.Name;

            return NoContent();
        }

        [HttpPatch("{cityId}/pointsofinterest/{id}")]
        public IActionResult UpdatePointsOfInterestPartially(int cityId, int id, [FromBody] JsonPatchDocument<PointsOfInterestForUpdationDTO> patchDoc)
        {
            if (patchDoc == null) return BadRequest();

            var city = _cityInfoRepo.GetCity(cityId);
            if (city == null) { return NotFound(); }

            var pointofinterest = city.PointOfInterest.FirstOrDefault(p => p.id == id);
            if (pointofinterest == null) { return NotFound(); }

            var pointOfInterestToPatch = new PointsOfInterestForUpdationDTO()
            {
                Name = pointofinterest.Name
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointofinterest.Name = pointOfInterestToPatch.Name;

            return NoContent();
        }

        [HttpDelete("{cityId}/pointsofinterest/{id}")]
        public IActionResult DeletePointOfInterest(int cityId, int Id)
        {
            var city = _cityInfoRepo.GetCity(cityId);
            if (city == null)
            {
                return NotFound();
            }

            var pointofinterest = city.PointOfInterest.FirstOrDefault(p => p.id == Id);
            if (pointofinterest == null) { return NotFound(); }

            city.PointOfInterest.Remove(pointofinterest);

            _mailService.SendMail("New subject", "New message");

            return NoContent();
        }
    }
}
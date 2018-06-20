using Microsoft.AspNetCore.Mvc;
using CityInfo.API.Entities;

namespace CityInfo.API.Controllers
{
    public class DummyController:Controller
    {
        private CityInfo.API.Entities.CityContext _ctx;

        public DummyController(CityInfo.API.Entities.CityContext ctx)
        {
            _ctx=ctx;
        }

        [HttpGet]
        [Route("api/GetDBCreated")]
        public IActionResult GetDBCreated()
        {
            return Ok();
        }
    }
}
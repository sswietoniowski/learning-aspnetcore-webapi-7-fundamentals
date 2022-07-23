using CityInfo.API.DataAccess.Data;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly CitiesDataStore _citiesDataStore;

        public CitiesController(ILogger<CitiesController> logger, CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            // DI with constructor is preferred but you might still get the service like this:
            //HttpContext.RequestServices.GetService(typeof(ILogger<CitiesController>));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            _logger.LogInformation($"Called: {nameof(GetCities)}");

            return Ok(_citiesDataStore.Cities);
        }

        [HttpGet("{id}")]
        public ActionResult<CityDto> GetCity(int id)
        {
            // Passing Data to the API (complex binding mechanism)
            // [FromBody] - inferred for complex types
            // [FromHeader] - not inferred
            // [FromRoute] - inferred for any action parameter name matching a parameter in the route template
            // [FromQuery] - inferred for any other action parameters
            // [FromForm] - inferred for action parameters of type IFormFile and IFormFileCollection

            // Status Codes:
            // Level 100 - Informational
            // Level 200 - OK (200 OK, 201 Created, 204 No Content)
            // Level 300 - Redirected
            // Level 400 - Client's Error (400 Bad Request, 401 Not Authorized, 403 Forbidden, 404 Not Found, 406 Not Acceptable, 409 Conflict)
            // Level 500 - Server's Error (500 Internal Server Error)

            _logger.LogInformation($"Called: {nameof(GetCity)}");

            var city = _citiesDataStore.Cities.FirstOrDefault(city => city.Id == id);

            if (city is null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}

using CityInfo.API.DataAccess.Data;
using CityInfo.API.Models;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        [HttpGet]
        public ActionResult<IEnumerable<CityDto>> GetCities()
        {
            return Ok(CitiesDataStore.Current.Cities);
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

            var city = CitiesDataStore.Current.Cities.FirstOrDefault(city => city.Id == id);

            if (city is null)
            {
                return NotFound();
            }

            return Ok(city);
        }
    }
}

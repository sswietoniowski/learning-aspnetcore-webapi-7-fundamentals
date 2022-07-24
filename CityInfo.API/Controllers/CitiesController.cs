using AutoMapper;
using CityInfo.API.DataAccess.Data;
using CityInfo.API.DataAccess.Repositories;
using CityInfo.API.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [ApiController]
    [Route("api/cities")]
    public class CitiesController : ControllerBase
    {
        private readonly ILogger<CitiesController> _logger;
        private readonly ICityInfoRepository _cityInfoRepository;
        private readonly IMapper _mapper;

        public CitiesController(ILogger<CitiesController> logger, CitiesDataStore citiesDataStore, ICityInfoRepository cityInfoRepository, IMapper mapper)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            // DI with constructor is preferred but you might still get the service like this:
            //HttpContext.RequestServices.GetService(typeof(ILogger<CitiesController>));
            _cityInfoRepository = cityInfoRepository ?? throw new ArgumentNullException(nameof(cityInfoRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityWithoutPointOfInterestDto>>> GetCities()
        {
            _logger.LogInformation($"Called: {nameof(GetCities)}");

            var cities = await _cityInfoRepository.GetCitiesAsync();
            var citiesDto = _mapper.Map<IEnumerable<CityWithoutPointOfInterestDto>>(cities);

            return Ok(citiesDto);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCity(int id, [FromQuery] bool includePointsOfInterest = false)
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

            var city = await _cityInfoRepository.GetCityAsync(id, includePointsOfInterest);

            if (city is null)
            {
                return NotFound();
            }

            if (includePointsOfInterest)
            {
                var cityDto = _mapper.Map<CityDto>(city);
                return Ok(cityDto);
            }
            else
            {
                var cityDto = _mapper.Map<CityWithoutPointOfInterestDto>(city);
                return Ok(cityDto);
            }
        }
    }
}

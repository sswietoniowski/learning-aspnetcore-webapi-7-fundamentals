using CityInfo.API.DataAccess.Data;
using CityInfo.API.Models;
using CityInfo.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace CityInfo.API.Controllers
{
    [Route("api/cities/{cityId}/pointsofinterest")]
    [ApiController]
    public class PointsOfInterestController : ControllerBase
    {
        private readonly ILogger<PointsOfInterestController> _logger;
        private readonly IMailService _mailService;
        private readonly CitiesDataStore _citiesDataStore;

        public PointsOfInterestController(ILogger<PointsOfInterestController> logger, IMailService mailService, CitiesDataStore citiesDataStore)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mailService = mailService ?? throw new ArgumentNullException(nameof(mailService));
            _citiesDataStore = citiesDataStore ?? throw new ArgumentNullException(nameof(citiesDataStore));
        }

        [HttpGet]
        public ActionResult<IEnumerable<PointOfInterestDto>> GetPointsOfInterest(int cityId)
        {
            try
            {
                _logger.LogInformation($"Called: {nameof(GetPointsOfInterest)}");

                var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

                if (city is null)
                {
                    // Logged events have severity (in an order from the least important to the most important):
                    // Trace
                    // Debug
                    // Information
                    // Warning
                    // Error
                    // Critical
                    _logger.LogWarning($"City with id {cityId} wasn't found when accessing points of interest");

                    return NotFound();
                }

                return Ok(city.PointsOfInterest);
            }
            catch (Exception exception)
            {
                _logger.LogCritical($"Exception while getting points of interest for city with id {cityId}.", exception);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "A problem happened while handling your request.");
            }
        }

        [HttpGet("{pointofinterestid}", Name = "GetPointOfInterest")]
        public ActionResult<PointOfInterestDto> GetPointOfInterest(int cityId, int pointOfInterestId)
        {
            _logger.LogInformation($"Called: {nameof(GetPointOfInterest)}");

            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            var pointOfInterest =
                city.PointsOfInterest.FirstOrDefault(pointOfInterest => pointOfInterest.Id == pointOfInterestId);

            if (pointOfInterest is null)
            {
                return NotFound();
            }

            return Ok(pointOfInterest);
        }

        [HttpPost]
        public ActionResult<PointOfInterestDto> CreatePointOfInterest(int cityId,
            [FromBody] PointOfInterestForCreationDto pointOfInterest)
        {
            //// Not necessary due to the ApiController attribute, that causes model checking
            //if (!ModelState.IsValid)
            //{
            //    return BadRequest(ModelState);
            //}

            // Built-in validations are OK for simple use cases, in a more advanced scenario, we might use
            // something like FluentValidation to validate the model.

            _logger.LogInformation($"Called: {nameof(CreatePointOfInterest)}");

            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            // for demo purposes only :-)!
            var maxPointOfInterestId = _citiesDataStore.Cities.SelectMany(
                c => c.PointsOfInterest).Max(p => p.Id);

            var finalPointOfInterest = new PointOfInterestDto()
            {
                Id = ++maxPointOfInterestId,
                Name = pointOfInterest.Name,
                Description = pointOfInterest.Description
            };

            city.PointsOfInterest.Add(finalPointOfInterest);

            return CreatedAtRoute(
                "GetPointOfInterest",
                new
                {
                    cityId,
                    pointOfInterestId = finalPointOfInterest.Id
                }, finalPointOfInterest);
        }

        [HttpPut("{pointofinterestid}")]
        public IActionResult UpdatePointOfInterest(int cityId, int pointOfInterestId,
            [FromBody] PointOfInterestForUpdateDto pointOfInterest)
        {
            _logger.LogInformation($"Called: {nameof(UpdatePointOfInterest)}");

            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(
                p => p.Id == pointOfInterestId);

            if (pointOfInterestFromStore is null)
            {
                return NotFound();
            }

            pointOfInterestFromStore.Name = pointOfInterest.Name;
            pointOfInterestFromStore.Description = pointOfInterest.Description;

            return NoContent();
        }

        [HttpPatch("{pointofinterestid}")]
        public IActionResult PartiallyUpdatePointOfInterest(int cityId, int pointOfInterestId,
            [FromBody] JsonPatchDocument<PointOfInterestForUpdateDto> patchDoc)
        {
            _logger.LogInformation($"Called: {nameof(PartiallyUpdatePointOfInterest)}");

            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(
                p => p.Id == pointOfInterestId);

            if (pointOfInterestFromStore is null)
            {
                return NotFound();
            }

            var pointOfInterestToPatch = new PointOfInterestForUpdateDto()
            {
                Name = pointOfInterestFromStore.Name,
                Description = pointOfInterestFromStore.Description
            };

            patchDoc.ApplyTo(pointOfInterestToPatch, ModelState);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            //if (pointOfInterestToPatch.Name is null)
            //{
            //    ModelState.AddModelError("Name", "Name is required");
            //}

            TryValidateModel(pointOfInterestToPatch);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            pointOfInterestFromStore.Name = pointOfInterestToPatch.Name;
            pointOfInterestFromStore.Description = pointOfInterestToPatch.Description;

            return NoContent();
        }

        [HttpDelete("{pointofinterestid}")]
        public IActionResult DeletePointOfInterest(int cityId, int pointOfInterestId)
        {
            _logger.LogInformation($"Called: {nameof(DeletePointOfInterest)}");

            var city = _citiesDataStore.Cities.FirstOrDefault(c => c.Id == cityId);

            if (city is null)
            {
                return NotFound();
            }

            var pointOfInterestFromStore = city.PointsOfInterest.FirstOrDefault(
                p => p.Id == pointOfInterestId);

            if (pointOfInterestFromStore is null)
            {
                return NotFound();
            }

            city.PointsOfInterest.Remove(pointOfInterestFromStore);

            _mailService.Send("Point of interest deleted.",
                $"Point of interest {pointOfInterestFromStore.Name} with id {pointOfInterestId} was deleted.");

            return NoContent();
        }
    }
}

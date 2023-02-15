using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.DTOs;

/// <summary>
/// A DTO for a city with points of interest
/// </summary>
public class CityDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string? Description { get; set; }
    public int NumberOfPointsOfInterest
    {
        get
        {
            return PointsOfInterest.Count;
        }
    }
    public ICollection<PointOfInterestDto> PointsOfInterest { get; set; } = new List<PointOfInterestDto>();
}

/// <summary>
/// A DTO for a city without points of interest
/// </summary>
public class CityWithoutPointOfInterestDto
{
    /// <summary>
    /// The id of the city
    /// </summary>
    public int Id { get; set; }
    /// <summary>
    /// The name of the city
    /// </summary>
    public string Name { get; set; } = string.Empty;
    /// <summary>
    /// The description of the city
    /// </summary>
    public string? Description { get; set; }
}
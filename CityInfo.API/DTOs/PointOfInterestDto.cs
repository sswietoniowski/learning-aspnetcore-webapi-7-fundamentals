using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.DTOs;

public class PointOfInterestDto
{
    public int Id { get; set; }

    public string Name { get; set; } = string.Empty;

    public string? Description { get; set; }
}

public class PointOfInterestForCreationDto
{
    [Required(ErrorMessage = "You should provide a name value for the point of interest.")]
    [MaxLength(64)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(256)]
    public string? Description { get; set; }
}

public class PointOfInterestForUpdateDto
{
    [Required(ErrorMessage = "You should provide a name value for the point of interest.")]
    [MaxLength(64)]
    public string Name { get; set; } = string.Empty;

    [MaxLength(256)]
    public string? Description { get; set; }
}
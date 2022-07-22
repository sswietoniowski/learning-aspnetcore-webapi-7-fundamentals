using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class PointOfInterestDto
    {
        [Key]
        public int Id { get; set; }

        [MaxLength(64)]
        public string Name { get; set; } = string.Empty;

        [MaxLength(256)]
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
}

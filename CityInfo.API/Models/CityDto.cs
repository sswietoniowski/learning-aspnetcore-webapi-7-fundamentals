using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Models
{
    public class CityDto
    {
        [Key]
        public int Id { get; set; }

        [Required] [MaxLength(64)] 
        public string Name { get; set; } = string.Empty;
        
        [MaxLength(256)]
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
}

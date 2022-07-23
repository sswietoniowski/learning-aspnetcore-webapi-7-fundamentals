using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CityInfo.API.DataAccess.Entities;

public class PointOfInterest
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }
    [Required]
    [MaxLength(64)]
    public string Name { get; set; }
    public string? Description { get; set; }

    [ForeignKey(nameof(CityId))]
    public City? City { get; set; }
    [Required]
    public int CityId { get; set; }

    public PointOfInterest(string name)
    {
        Name = name;
    }
}
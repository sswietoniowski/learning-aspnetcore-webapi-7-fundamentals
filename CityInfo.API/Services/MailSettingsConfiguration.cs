using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Services;

public class MailSettingsConfiguration
{
    [Required]
    public string MailTo { get; set; }
    [Required]
    public string MailFrom { get; set; }
}
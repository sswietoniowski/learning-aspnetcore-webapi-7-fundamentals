using System.ComponentModel.DataAnnotations;

namespace CityInfo.API.Services;

public class MailSettingsConfiguration
{
    [Required]
    public string MailTo { get; set; } = string.Empty;
    [Required]
    public string MailFrom { get; set; } = string.Empty;
}
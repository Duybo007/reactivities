namespace Application.Activities.DTOs;
using System.ComponentModel.DataAnnotations;

// public class CreateActivityDto : BaseActivityDto
public class CreateActivityDto
{
    [Required]
    public string Title { get; set; } = string.Empty;
    [Required(ErrorMessage = "The Date field is required.")]
    public DateTime? Date { get; set; }
    [Required]
    public string Description { get; set; } = string.Empty;
    [Required]
    public string Category { get; set; } = string.Empty;
    public bool Iscanceled { get; set; }
    // location props
    [Required]
    public string City { get; set; } = string.Empty;
    [Required]
    public string Venue { get; set; } = string.Empty;
    [Required(ErrorMessage = "The Latitude field is required.")]
    [Range(-90, 90, ErrorMessage = "Latitude must be between -90 and 90.")]
    public double? Latitude { get; set; }
    [Required(ErrorMessage = "The Longitude field is required.")]
    [Range(-180, 180, ErrorMessage = "Longitude must be between -180 and 180.")]
    public double? Longitude { get; set; }
}

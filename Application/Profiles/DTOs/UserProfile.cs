
namespace Application.Profiles.DTOs;

public class UserProfile
{
    public required string Id { get; set; }
    public required string DisplayName { get; set; }
    public required string Bio { get; set; }
    public string? ImageUrl { get; set; }
}

using System.ComponentModel.DataAnnotations;
using Shared.Models.User.Dtos.GetDto;

namespace Shared.Models.TravelPlan.Dtos.GetDto;

public class TravelPlanDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    
    [Required]
    public string FromCountry { get; set; } = string.Empty;
    
    [Required]
    public string ToCountry { get; set; } = string.Empty;
    
    [Required]
    public DateTime TravelDate { get; set; }
    
    public string Description { get; set; } = string.Empty;
    
    public UserPublicDto? User { get; set; }
}


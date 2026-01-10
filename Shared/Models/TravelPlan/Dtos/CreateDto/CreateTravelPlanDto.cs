using System.ComponentModel.DataAnnotations;
namespace Shared.Models.TravelPlan.Dtos.CreateDto;

public class CreateTravelPlanDto
{
  
        [Required]
        [StringLength(100)]
        public string FromCountry { get; set; } = string.Empty;
    
        [Required]
        [StringLength(100)]
        public string ToCountry { get; set; } = string.Empty;
    
        [Required]
        public DateTime TravelDate { get; set; }
    
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
    
}
using System;
using System.ComponentModel.DataAnnotations;

namespace Shared.Models.TravelPlan.Model;

public class TravelPlan
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid UserId { get; set; }
    public Shared.Models.User.Models.User? User { get; set; }
    
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
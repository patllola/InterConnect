using System.ComponentModel.DataAnnotations;
using Shared.Models.User.Models;

namespace Shared.Models.TravelPlan.Model;

public class TravelPlan
{
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid UserId { get; set; }
        public User? User { get; set; }
        
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
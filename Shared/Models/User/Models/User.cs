using System.ComponentModel.DataAnnotations;
using TravelPlanModel = Shared.Models.TravelPlan.Model.TravelPlan;

namespace Shared.Models.User.Models;

public class User
{
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        
        [Required]
        [EmailAddress]
        public string Email { get; set; } = string.Empty;
        
        [Required]
        public string PasswordHash { get; set; } = string.Empty;
        
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public ICollection<TravelPlanModel> TravelPlans { get; set; } = new List<TravelPlanModel>();
}
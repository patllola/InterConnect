using System;

namespace Shared.Models.DTOs
{
    public class UserPublicDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        // Email, PhoneNumber, Address are HIDDEN here
    }

    public class UserPrivateDto : UserPublicDto
    {
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }

    public class TravelPlanDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public UserPublicDto? User { get; set; }
        public string FromCountry { get; set; } = string.Empty;
        public string ToCountry { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}

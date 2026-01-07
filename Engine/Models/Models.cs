using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace Engine.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        
        // Personal information to be hidden until invitation accepted
        public string PhoneNumber { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;

        public ICollection<TravelPlan> TravelPlans { get; set; } = new List<TravelPlan>();
    }

    public class TravelPlan
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User? User { get; set; }
        public string FromCountry { get; set; } = string.Empty;
        public string ToCountry { get; set; } = string.Empty;
        public DateTime TravelDate { get; set; }
        public string Description { get; set; } = string.Empty;
    }

    public class HelpRequest
    {
        public Guid Id { get; set; }
        public Guid SeekerId { get; set; }
        public User? Seeker { get; set; }
        public Guid? HelperId { get; set; }
        public User? Helper { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Status { get; set; } = "Pending"; // Pending, Accepted, Completed, Cancelled
        public decimal Price { get; set; }
        public bool IsPaid { get; set; }
        public DateTime? CompletedAt { get; set; }
    }

    public class ChatMessage
    {
        public Guid Id { get; set; }
        public Guid HelpRequestId { get; set; }
        public HelpRequest? HelpRequest { get; set; }
        public Guid SenderId { get; set; }
        public string Message { get; set; } = string.Empty;
        public DateTime SentAt { get; set; } = DateTime.UtcNow;
    }
}

using System.ComponentModel.DataAnnotations;
using Shared.Models.HelpRequest.Enums;

namespace Shared.Models.HelpRequest.Models;

public class HelpRequest
{
        [Key]
        public Guid Id { get; set; }
        
        [Required]
        public Guid SeekerId { get; set; }
        public Shared.Models.User.Models.User? Seeker { get; set; }
        
        public Guid? HelperId { get; set; }
        public Shared.Models.User.Models.User? Helper { get; set; }
        
        [Required]
        [StringLength(1000)]
        public string Description { get; set; } = string.Empty;
        
        [Required]
        [EnumDataType(typeof(HelpRequestStatus))]
        public HelpRequestStatus Status { get; set; } 
        
        [Required]
        [Range(0, 1000000)]
        public decimal Price { get; set; }
        
        public bool IsPaid { get; set; }
        
        public DateTime? CompletedAt { get; set; }
}
using System;
using System.ComponentModel.DataAnnotations;
using Shared.Models.HelpRequest.Enums;

namespace Shared.Models.HelpRequest.Dtos.GetDto;

public class GetHelpRequestDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public Guid SeekerId { get; set; }
    
    public Shared.Models.User.Models.User? Seeker { get; set; }
    
    public Shared.Models.User.Models.User? Helper { get; set; }
    
    [Required]
    public string Description { get; set; } = string.Empty;
    
    [Required]
    [EnumDataType(typeof(HelpRequestStatus))]
    public HelpRequestStatus Status { get; set; }
    
    [Required]
    public decimal Price { get; set; }
    
    [Required]
    public bool IsPaid { get; set; }
    
    public DateTime? CompletedAt { get; set; }
}
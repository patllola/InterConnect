using System.ComponentModel.DataAnnotations;
using Shared.Models.HelpRequest.Models;
using Shared.Models.ChatMessages.Enums;

namespace Shared.Models.ChatMessages.Models;

public class ChatMessage
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid HelpRequestId { get; set; }
    public HelpRequest? HelpRequest { get; set; }
    
    [Required]
    public Guid SenderId { get; set; }
    
    [Required]
    public string Message { get; set; } = string.Empty;
    
    public DateTime SentAt { get; set; } = DateTime.UtcNow;
    
    public HelpRequestType Type { get; set; }
}
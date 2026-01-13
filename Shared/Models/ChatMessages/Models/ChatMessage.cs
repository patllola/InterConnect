using System;
using System.ComponentModel.DataAnnotations;
using Shared.Models.ChatMessages.Enums;
using HelpRequestModel = Shared.Models.HelpRequest.Models.HelpRequest;

namespace Shared.Models.ChatMessages.Models;

public class ChatMessage
{
    [Key]
    public Guid Id { get; set; }
    
    [Required]
    public Guid HelpRequestId { get; set; }
    public HelpRequestModel? HelpRequest { get; set; }
    
    [Required]
    public Guid SenderId { get; set; }
    
    [Required]
    [StringLength(2000)]
    public string Message { get; set; } = string.Empty;
    
    [Required]
    public DateTime SentAt { get; set; } = DateTime.UtcNow;

    [Required]
    [EnumDataType(typeof(ChatRequestType))]
    public ChatRequestType Type { get; set; }
}
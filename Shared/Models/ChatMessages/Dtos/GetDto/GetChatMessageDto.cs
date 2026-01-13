using System;
using System.ComponentModel.DataAnnotations;
using Shared.Models.ChatMessages.Enums;

namespace Shared.Models.ChatMessages.Dtos.GetDto;

public class GetChatMessageDto
{
    [Required]
    public Guid Id { get; set; }
    
    [Required]
    public Guid HelpRequestId { get; set; }
    
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
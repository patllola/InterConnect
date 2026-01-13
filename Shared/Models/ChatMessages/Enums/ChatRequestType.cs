using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Shared.Models.ChatMessages.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ChatRequestType
{
    [Description("Message")]
    Message = 0,
    
    [Description("Call")]
    Call = 1
}
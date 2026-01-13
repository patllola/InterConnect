using System;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Shared.Models.HelpRequest.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HelpRequestStatus
{
    [Description("Pending")]
    Pending = 1,
    
    [Description("Accepted")]
    Accepted = 2,
    
    [Description("Completed")]
    Completed = 3,
    
    [Description("Cancelled")]
    Cancelled = 4
}
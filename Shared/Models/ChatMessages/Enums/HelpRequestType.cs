using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Shared.Models.ChatMessages.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HelpRequestType
{
    [Description("TravelHelp")]
    TravelHelp = 0,
    
    [Description("Other")]
    Other = 1
}
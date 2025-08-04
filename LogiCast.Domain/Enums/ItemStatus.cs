using System.Text.Json.Serialization;

namespace LogiCast.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ItemStatus
{
    BelowMin,
    Normal,
    Max,
    Alert
}
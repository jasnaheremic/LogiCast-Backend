using System.Text.Json.Serialization;

namespace LogiCast.Domain.Enums;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ItemUnit
{
    Piece,
    Liter,
    Kilogram,
    Meter,
    Set
}
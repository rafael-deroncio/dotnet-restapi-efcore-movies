using System.ComponentModel;

namespace MovieMania.Domain.Enums;

public enum ResponseType
{
    [Description("Warning")]
    Warning,

    [Description("Error")]
    Error,

    [Description("Fatal")]
    Fatal,
}
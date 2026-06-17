using System.Text.Json.Serialization;

namespace ColorTypeGuide.AiClient;

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum HairTone
{
    Fair,
    Medium,
    Dark
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SkinTone
{
    Fair,
    Medium,
    Deep
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Undertone
{
    Cool,
    Warm,
    Neutral
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum Luminance
{
    Bright,
    Soft,
    Neutral
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum ContrastLevel
{
    Low,
    Medium,
    High
}

[JsonConverter(typeof(JsonStringEnumConverter))]
public enum SeasonalColorType
{
    LightSpring,
    WarmSpring,
    BrightSpring,
    SoftSpring,
    LightSummer,
    CoolSummer,
    SoftSummer,
    DeepSummer,
    SoftAutumn,
    WarmAutumn,
    DeepAutumn,
    LightAutumn,
    CoolWinter,
    BrightWinter,
    DeepWinter,
    SoftWinter
}

public record ColorTypeAnalysis(
    [property: JsonPropertyName("analyzedParameters")]
    AnalyzedParameters? AnalyzedParameters,

    [property: JsonPropertyName("colorType")]
    SeasonalColorType? ColorType
);

public record AnalyzedParameters(
    [property: JsonPropertyName("eyeColor")]
    string? EyeColor,

    [property: JsonPropertyName("hair")]
    HairAnalysis? Hair,

    [property: JsonPropertyName("skin")]
    SkinAnalysis? Skin,

    [property: JsonPropertyName("contrastLevel")]
    ContrastLevel? ContrastLevel
);

public record HairAnalysis(
    [property: JsonPropertyName("tone")]
    HairTone? Tone,

    [property: JsonPropertyName("color")]
    string? Color
);

public record SkinAnalysis(
    [property: JsonPropertyName("tone")]
    SkinTone? Tone,

    [property: JsonPropertyName("undertone")]
    Undertone? Undertone,

    [property: JsonPropertyName("luminance")]
    Luminance? Luminance
);

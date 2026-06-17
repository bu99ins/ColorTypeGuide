using System.Text.Json.Serialization;

namespace ColorTypeGuide.AiClient;

public record ColorTypeAnalysis(
    [property: JsonPropertyName("analyzedParameters")]
    AnalyzedParameters? AnalyzedParameters,

    [property: JsonPropertyName("colorType")]
    string? ColorType
);

public record AnalyzedParameters(
    [property: JsonPropertyName("eyeColor")]
    string? EyeColor,

    [property: JsonPropertyName("hair")]
    HairAnalysis? Hair,

    [property: JsonPropertyName("skin")]
    SkinAnalysis? Skin,

    [property: JsonPropertyName("contrastLevel")]
    string? ContrastLevel
);

public record HairAnalysis(
    [property: JsonPropertyName("tone")]
    string? Tone,

    [property: JsonPropertyName("color")]
    string? Color
);

public record SkinAnalysis(
    [property: JsonPropertyName("tone")]
    string? Tone,

    [property: JsonPropertyName("undertone")]
    string? Undertone,

    [property: JsonPropertyName("luminance")]
    string? Luminance
);

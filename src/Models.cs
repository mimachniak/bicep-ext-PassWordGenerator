using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Azure.Bicep.Types.Concrete;
using Bicep.Local.Extension.Types.Attributes;

public class PassWordProperties
{
    [TypeProperty("Password will include lower cases.")]
    [JsonPropertyName("includeLower")]
    public bool IncludeLower { get; set; }

    [TypeProperty("Password will include Upper cases.")]
    [JsonPropertyName("includeUpper")]
    public bool IncludeUpper { get; set; }

    [TypeProperty("Password will include Digits.")]
    [JsonPropertyName("includeDigits")]
    public bool IncludeDigits { get; set; }

    [TypeProperty("Password will include special characters.")]
    [JsonPropertyName("includeSpecial")]
    public bool IncludeSpecial { get; set; }

    [TypeProperty("Password length.")]
    [JsonPropertyName("passwordLength")]
    public int PasswordLength { get; set; }
}

[ResourceType("GeneratedPassword")]
public class GeneratedPassword
{
    [Required]
    [TypeProperty("Resource name for the generated password.")]
    [JsonPropertyName("name")]
    public string Name { get; set; } = string.Empty;

    [Required]
    [TypeProperty("Password generation properties.")]
    [JsonPropertyName("properties")]
    public PassWordProperties Properties { get; set; } = new PassWordProperties();

    [TypeProperty("Your generated password.")]
    [JsonPropertyName("output")]
    public string? Output { get; set; }
}

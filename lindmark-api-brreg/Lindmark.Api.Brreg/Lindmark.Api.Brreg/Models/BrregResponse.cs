using System.Text.Json.Serialization;

namespace Lindmark.Api.Brreg.Models;

public class BrregResponse
{
    [JsonPropertyName("organisasjonsnummer")]
    public required string OrgNr { get; set; }
    
    [JsonPropertyName("navn")]
    public required string Name { get; set; }
    
    [JsonPropertyName("slettedato")]
    public string? DeletionDate { get; set; }
    
    [JsonPropertyName("konkurs")]
    public bool? Bankrupt { get; set; }
    
    [JsonPropertyName("underAvvikling")]
    public bool? UnderLiquidation { get; set; }
    
    [JsonPropertyName("underTvangsavviklingEllerTvangsopplosning")]
    public bool? ForcedLiquidationOrDissolution { get; set; }
    
    [JsonPropertyName("antallAnsatte")]
    public int? NumberOfEmployees { get; set; }
    
    [JsonPropertyName("organisasjonsform")]
    public required OrganizationType OrganizationType { get; set; }
    
    [JsonPropertyName("naeringskode1")]
    public IndustryCode? IndustryCode { get; set; }
}

public class OrganizationType
{
    [JsonPropertyName("kode")]
    public required string Code { get; set; }
    
}

public class IndustryCode
{
    [JsonPropertyName("kode")]
    public string? Code { get; set; }
}

public class BrregDeletedResponse
{
    [JsonPropertyName("organisasjonsnummer")]
    public required string OrgNr { get; set; }
    
    [JsonPropertyName("slettedato")]
    public required string DeletionDate { get; set; }
}
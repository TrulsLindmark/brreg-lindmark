using System.Text.Json.Serialization;
using CsvHelper.Configuration.Attributes;

namespace Lindmark.Api.Brreg.Models;
public class Company
{
    [Index(0)]
    [Name("OrgNo")]
    public required string OrgNr { get; set; }
    
    [Index(1)]
    [Name("FirmaNavn")]
    public required string Name { get; set; }
    
    [Index(2)]
    [Name("Status")]
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public required Status Status { get; set; }
    
    [Index(3)]
    [Name("AntallAnsatte")]
    public int? NumberOfEmployees { get; set; }
    
    [Index(4)]
    [Name("OrganisasjonsformKode")]
    public string? OrganizationTypeCode { get; set; }
    
    [Index(5)]
    [Name("Naeringskode")]
    public string? IndustryCode { get; set; }
}

public class CompanyInput
{
    [Index(0)]
    [Name("FirmaNavn")]
    public required string OrgNr { get; set; }
    
    [Index(1)]
    [Name("OrgNr")]
    public required string Name { get; set; }
}

public enum Status
{
    Aktiv,
    UnderAvvikling,
    Konkurs,
    Slettet,
    Feil
}
namespace Lindmark.Api.Brreg.Models;

public class CompanySummary
{
    public required CompanyStatusCount StatusCount { get; set; }
    public required CompanyEmployeeDistribution EmployeeDistribution { get; set; }
    public required CompanyOrganizationTypeDistribution OrganizationTypeDistribution { get; set; }
    
}

public class CompanyStatusCount
{
    public required int Aktiv  { get; set; }
    public required int UnderAvvikling { get; set; }
    public required int Konkurs { get; set; }
    public required int Slettet { get; set; }
    public required int Feil { get; set; }
}

public class CompanyOrganizationTypeDistribution
{
    public required List<OrganizationTypeDistribution> OrganizationTypeDistribution { get; set; }
}

public class OrganizationTypeDistribution
{
    public required string Code { get; set; }
    public required double Percentage { get; set; }
}
public class CompanyEmployeeDistribution
{
    public required int ZeroOrNull { get; set; }
    public required int OneToNine { get; set; }
    public required int TenToFortyNine { get; set; }
    public required int MoreThanFifty { get; set; }
}